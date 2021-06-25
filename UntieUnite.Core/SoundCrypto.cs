using System;
using System.IO;

namespace UntieUnite.Core
{
    public static class SoundCrypto
    {
        private static readonly uint[] TEAKey = { 0x6F83F81C, 0x85594EF2, 0xAD808738, 0x6CF5D44C };
        private static readonly uint[] Tweaks = { 0x26F005E7, 0xA7F34B3C, 0x8CCBA36C, 0xFBAC2D78, 0xF84716C9, 0x6FF44239, 0x9BC9BEF0, 0x5DFC4FA6 };

        private static readonly uint[] BaseKeySwitch = { 0x3A07B511, 0x6D19428A, 0x9C5F2248, 0x9F3379EC };
        private static readonly uint[] SwitchTweaks = { 0x63BEFBE4, 0xDD644155, 0xB4E76C6E, 0xC2E63A6B, 0x0242EAB6, 0xDFA44BE0, 0x8AFC88A9, 0xC8C8C446 };

        public static uint[] GetSwitchKey(string filename, uint header)
        {
            var h0 = (0x14F3CD8Bul * (ulong)filename.Length + 0x346797A3ul) & 0xFFFFFFFFul;
            var h1 = (0x14F3CD8Bul * ((0xCB98685Cu - 0x14F3CD8Bu * (uint)filename.Length) & 0xFFFFFFFF) + 0x346797A3ul) & 0xFFFFFFFFul;

            foreach (var c in filename)
            {
                h0 = 0x10F4AB * h0 + 0x17720F33 * (ulong)c;
                h1 = 0x5994A7 * h1 + 0x17B32437 * (ulong)c;
            }

            header &= ~0xFu;

            var key = new uint[8];
            key[0] = BaseKeySwitch[0] ^ (uint)(h0 >> 32) ^ header;
            key[1] = BaseKeySwitch[1] ^ (uint)(h0 >>  0) ^ header;
            key[2] = BaseKeySwitch[2] ^ (uint)(h1 >> 32) ^ header;
            key[3] = BaseKeySwitch[3] ^ (uint)(h1 >>  0) ^ header;
            key[4] = 0x14F3CD8Bu * key[0] + 0x346797A3u;
            key[5] = 0x14F3CD8Bu * key[1] + 0x346797A3u;
            key[6] = 0x14F3CD8Bu * key[2] + 0x346797A3u;
            key[7] = 0x14F3CD8Bu * key[3] + 0x346797A3u;
            return key;
        }

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

        public static ulong SwitchTEADecrypt(uint[] key, ulong block, uint idx)
        {
            var v0 = (uint)(block >> 0);
            var v1 = (uint)(block >> 32);

            var x0 = SwitchTweaks[idx % SwitchTweaks.Length] ^ idx;
            var x1 = (0x8B * (x0 & 0xFF) - 0x5D) & 1u;

            var t0 = 0x14F3CD8B * x0 + 0x346797A3;
            var t1 = 0x9E3779B9 * x1;

            var key_ind = ((0x8B * (t0 & 0xFF) - 0x5D) & 3) + (((0x14F3CD8B * t0 + 0x346797A3) >> 31) & 1);

            v1 -= (((v0 << 4) ^ (v0 >> 5)) + v0) ^ (key[key_ind + ((t1 - 0x61C88647) & 3)] + t1 - 0x61C88647);
            v0 -= (((v1 << 4) ^ (v1 >> 5)) + v1) ^ (key[key_ind + (t1 & 3)] - 0x61C88647 * x1);

            if (x1 != 0)
            {
                v1 -= (((v0 << 4) ^ (v0 >> 5)) + v0) ^ (key[key_ind + (((0x9E3779B9 * (t0 & 1)) >> 11) & 3)] - 0x61C88647 * (t0 & 1));
                v0 -= (((v1 << 4) ^ (v1 >> 5)) + v1) ^ (key[key_ind + ((t1 + 0x61C88647) & 3)] + t1 + 0x61C88647);
            }

            v0 ^= t0;
            v1 ^= 0x14F3CD8B * t0 + 0x346797A3;

            return ((ulong)v0 << 0) | ((ulong)v1 << 32);
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

        public static byte[] DecryptSwitch(string filename, byte[] data)
        {
            var header = BitConverter.ToUInt32(data, 0);
            var decSize = data.Length - 4 - (header & 0xF);

            var key = GetSwitchKey(filename, header);

            var dec = new byte[decSize];
            for (var ofs = 0; ofs < decSize; ofs += 8)
            {
                var block = BitConverter.GetBytes(SwitchTEADecrypt(key, BitConverter.ToUInt64(data, 4 + ofs), (uint)ofs / 8));

                Buffer.BlockCopy(block, 0, dec, ofs, Math.Min(8, dec.Length - ofs));
            }

            return dec;
        }
    }
}
