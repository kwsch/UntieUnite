using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Security.Cryptography;
using Ionic.Zlib;

namespace UntieUnite.Core
{
    /// <summary>
    /// Decodes the ResMap protobuf data binary into something that can be loaded into a proto class.
    /// </summary>
    public class ResDecoder
    {
        public static readonly byte[] MasterKeyAndroid = { 0xB2, 0x7F, 0x19, 0x12, 0x8D, 0x5F, 0xCB, 0x75, 0xB0, 0xEA, 0x2A, 0x60, 0xCC, 0x03, 0xA2, 0x55 };
        public static readonly byte[] MagicAndPaddingAndroid = { 0x9D, 0x4C, 0x2D, 0x00 };
        public static readonly byte[] MasterKeySwitch = { 0x99, 0x64, 0xB1, 0xB0, 0x6B, 0x03, 0x8D, 0x7F, 0xB7, 0x7D, 0xB6, 0xA7, 0x54, 0x90, 0x8B, 0x73 };
        public static readonly byte[] MagicAndPaddingSwitch = { 0x22, 0x4A, 0x67, 0x00 };
        public static readonly byte[] MagicAndPaddingSwitch2 = { 0x22, 0x4A, 0xEF, 0x00 };

        private readonly AesCryptoServiceProvider _aesCrypto;
        private readonly AssetFormat _assetFormat;

        public static byte[] GetMasterKey(AssetFormat format) => format switch
        {
            AssetFormat.Android => MasterKeyAndroid,
            AssetFormat.Switch => MasterKeySwitch,
            _ => throw new ArgumentOutOfRangeException($"Invalid Asset Format ({format})")
        };

        public byte[] GetMasterKey() => GetMasterKey(_assetFormat);

        public ResDecoder(uint salt, AssetFormat format)
        {
            _aesCrypto = new AesCryptoServiceProvider { Key = GenerateDerivedKey(salt, format), IV = new byte[16] };
            _assetFormat = format;
        }

        private static bool IsValidMagic(byte[] archive, byte[] magic)
        {
            if (archive.Length < magic.Length)
                return false;

            for (var i = 0; i < 3; ++i)
            {
                if (archive[i] != magic[i])
                    return false;
            }

            return true;
        }

        public static AssetFormat GetAssetFormat(byte[] archive)
        {
            if (IsValidMagic(archive, MagicAndPaddingAndroid))
                return AssetFormat.Android;
            if (IsValidMagic(archive, MagicAndPaddingSwitch) || IsValidMagic(archive, MagicAndPaddingSwitch2))
                return AssetFormat.Switch;
            return AssetFormat.Invalid;
        }

        public static uint GetAssetSalt(string resName)
        {
            var hash = 0u;

            foreach (var c in resName.ToUpper())
                hash = (31 * hash) + c;

            return hash;
        }

        public bool TryDecryptBytes(byte[] archive, [NotNullWhen(true)] out byte[]? decrypted)
        {
            /* On default failure, return null. */
            decrypted = null;

            /* Check that the data is a resource archive. */
            if (GetAssetFormat(archive) == AssetFormat.Invalid)
                return false;

            /* Get the archive's padding. */
            var padding = archive[3];
            if (padding > 0x10)
                return false;

            /* Decrypt the archive. */
            ReadOnlySpan<byte> decSpan;
            try
            {
                decSpan = Decrypt(archive, 4, archive.Length - 4, padding);
            }
            catch (CryptographicException cex)
            {
                Console.WriteLine(cex);
                return false;
            }
            catch (InvalidOperationException iex)
            {
                Console.WriteLine(iex);
                return false;
            }

            var hashCode = BitConverter.ToUInt32(decSpan);
            var decData = decSpan[4..];
            if (hashCode != HashUtil.GetHashCode(decData))
                return false;

            decrypted = decData.ToArray();
            return true;
        }

        private ReadOnlySpan<byte> Decrypt(byte[] arr, int ofs, int count, byte padding)
        {
            using var ms = new MemoryStream(arr, ofs, count);
            using var cs = new CryptoStream(ms, _aesCrypto.CreateDecryptor(), CryptoStreamMode.Read);

            var dec = new byte[count];
            cs.Read(dec, 0, dec.Length);

            // Sanity check the padding at the end.
            for (var i = 0; i < padding; ++i)
            {
                if (dec[^(1+i)] != 0)
                    throw new InvalidOperationException();
            }

            return dec[..^padding];
        }

        // Helpers

        /// <summary>
        /// Key Derivation Function that simply XOR's the 32bit <see cref="salt"/> into the <see cref="AssetFormat"/> MasterKey.
        /// </summary>
        public static byte[] GenerateDerivedKey(uint salt, AssetFormat format)
        {
            var derived = (byte[])GetMasterKey(format).Clone();

            // Manually xor the uint's byte values into the key.
            for (var i = 0; i < derived.Length; ++i)
            {
                var shift = (i << 3) & 0x1F;
                derived[i] ^= (byte)(salt >> shift);
            }

            return derived;
        }

        /// <summary>
        /// Decrypts the input <see cref="data"/> using the provided <see cref="salt"/>.
        /// </summary>
        /// <remarks>Throws an <see cref="InvalidDataException"/> if the decryption fails.</remarks>
        public static byte[] Decrypt(uint salt, byte[] data)
        {
            if (data.Length == 0)
                return Array.Empty<byte>();

            var format = GetAssetFormat(data);

            if (format == AssetFormat.Invalid)
                return (byte[])data.Clone();

            var decoder = new ResDecoder(salt, format);
            if (!decoder.TryDecryptBytes(data, out var decrypted))
                throw new InvalidDataException();
            return decrypted;
        }

        /// <summary>
        /// Decrypts &amp; decompresses (via <see cref="DeflateStream"/>) the input <see cref="data"/> using the provided <see cref="salt"/>.
        /// </summary>
        /// <remarks>Throws an <see cref="InvalidDataException"/> if the decryption fails.</remarks>
        public static byte[] DecryptAndDecompress(uint salt, byte[] data)
        {
            if (data.Length == 0)
                return Array.Empty<byte>();

            if (GetAssetFormat(data) == AssetFormat.Invalid)
                return (byte[])data.Clone();

            var decrypted = Decrypt(salt, data);
            return DeflateStream.UncompressBuffer(decrypted);
        }
    }
}
