using System;
using System.IO;

namespace UntieUnite.Core
{
    public static class SoundCrypto
    {
        private static readonly uint[] TEAKey = { 0x6F83F81C, 0x85594EF2, 0xAD808738, 0x6CF5D44C };
        private static readonly uint[] Tweaks = { 0x26F005E7, 0xA7F34B3C, 0x8CCBA36C, 0xFBAC2D78, 0xF84716C9, 0x6FF44239, 0x9BC9BEF0, 0x5DFC4FA6 };

        public static ulong XTEADecrypt(ulong block)
        {
            // Perform one round (lol) of XTEA decryption.
            // Note that 64 rounds or more is standard/recommended.
            var v0 = (uint)(block >> 0);
            var v1 = (uint)(block >> 32);
            v1 -= (((v0 << 4) ^ (v0 >> 5)) + v0) ^ (TEAKey[3] - 0x61C88647);
            v0 -= (((v1 << 4) ^ (v1 >> 5)) + v1) ^ (TEAKey[0]);

            return ((ulong) v0 << 0) | ((ulong) v1 << 32);
        }

        public static ulong GetTweak(uint key, uint idx)
        {
            var tweak = key ^ idx ^ Tweaks[idx % Tweaks.Length];
            return ((ulong) tweak << 0) | ((ulong) (~tweak) << 32);
        }

        public static ulong DecryptBlock(uint tweakKey, ulong block, uint idx)
        {
            return XTEADecrypt(block) ^ GetTweak(tweakKey, idx);
        }

        public static bool IsSoundArchive(byte[] data)
        {
            if (data.Length < 12)
                return false;

            var header = BitConverter.ToUInt32(data, 0);
            var paddingBytes = header & 0xF;

            var rotate = (int)((2 * paddingBytes + 1) & 0x1C);
            var magic = (((header >> 4) << (28 - rotate)) & 0xFFFFFFF) | ((header >> 4) >> rotate);
            if (magic != 0xBA0CF89)
                return false;

            var dataSize = BitConverter.ToUInt32(data, 4) ^ header;

            return 12 + dataSize + paddingBytes == data.Length;
        }

        public static byte[] Decrypt(byte[] data)
        {
            if (!IsSoundArchive(data)) throw new InvalidDataException("Invalid sound archive");

            var decSize = BitConverter.ToUInt32(data, 0) ^ BitConverter.ToUInt32(data, 4);
            var tweakKey = BitConverter.ToUInt32(data, 8);

            var dec = new byte[decSize];

            for (var ofs = 0; ofs < decSize; ofs += 8)
            {
                var block = BitConverter.GetBytes(DecryptBlock(tweakKey, BitConverter.ToUInt64(data, 12 + ofs), (uint)ofs / 8));

                Buffer.BlockCopy(block, 0, dec, ofs, Math.Min(8, dec.Length - ofs));
            }

            return dec;
        }
    }
}
