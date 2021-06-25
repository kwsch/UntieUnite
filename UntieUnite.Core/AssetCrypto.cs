﻿using System;
using System.IO;
using System.Security.Cryptography;

namespace UntieUnite.Core
{
    /// <summary>
    /// Decrypts Unity Bundles (*.bundle) files by cleaning up the metadata at the top.
    /// </summary>
    public static class AssetCrypto
    {
        private static readonly byte[] MetaDataKey = {0x12, 0x16, 0x60, 0xCA, 0x64, 0x0C, 0xFD, 0xCE, 0xE1, 0x6D, 0x74, 0x3C, 0x0D, 0x1A, 0x99, 0x6A};

        private static readonly byte[] BlockInfoKey =
        {
            0x23, 0xE7, 0xF3, 0xB1, 0x0E, 0x78, 0xEC, 0xD1, 0x50, 0x7B, 0x6B, 0x17, 0x3F, 0x61, 0xC5, 0x79, 0x0C, 0x57,
            0x32, 0x1A, 0xF3, 0xB8, 0x6B, 0x68, 0xDE, 0x2A, 0x5F, 0x01, 0xBA, 0x98, 0x3A, 0x99, 0xC0, 0x54, 0x02, 0x24,
            0xF7, 0x9B, 0x09, 0x87, 0x23, 0xC4, 0x6F, 0x0E, 0x6C, 0x44, 0xFA, 0xDB, 0xFB, 0xE8, 0x85, 0xAB, 0xC2, 0x65,
            0x3C, 0x0E, 0xC4, 0x93, 0xF6, 0x6D, 0x0B, 0x8A, 0xD6, 0x11, 0x8D, 0xE3, 0x8F, 0x71, 0x52, 0x5D, 0x6E, 0xFC,
            0xFD, 0x29, 0x82, 0xB0, 0x1D, 0x13, 0x11, 0xAE, 0x5C, 0xD5, 0xA9, 0x1B, 0xF8, 0xCE, 0xFC, 0x79, 0x9C, 0x5A,
            0xD6, 0xCE, 0xFD, 0x0C, 0x64, 0xCA, 0x60, 0x16, 0x12, 0x31, 0x5B, 0x08, 0x3A, 0xCF, 0x04, 0x3E, 0xEA, 0x23,
            0xDC, 0x28, 0xFA, 0x20, 0xA5, 0xC0, 0xB8, 0x21, 0x73, 0x5E, 0x6C, 0x6A, 0x2B, 0x31, 0xE9, 0x6D, 0xBD, 0x9A,
            0x73, 0x11, 0x4C, 0xB1, 0x43, 0x3A, 0x8E, 0x28, 0xCE, 0xDC, 0x9B, 0xD4, 0x31, 0xCF, 0x77, 0x1D, 0xE4, 0x9F,
            0x8A, 0x8B, 0x0A, 0xB2, 0x4E, 0xC0, 0x8D, 0xDD, 0x74, 0x0B, 0x56, 0xCF, 0xB7, 0xEE, 0xD5, 0x74, 0xA7, 0xB5,
            0x1B, 0xA1, 0xA9, 0x85, 0xCB, 0x45, 0x68, 0xFF, 0x1F, 0x59, 0xFB, 0xCD, 0x42, 0xDA, 0xFF, 0x59, 0x37, 0x05,
            0xE7, 0xDC, 0x9E, 0x12, 0xBD, 0x1B, 0x87, 0xBB, 0x97, 0x02, 0x9A, 0xC2, 0x04, 0x66, 0xD3, 0xBE, 0xA7, 0x2C,
            0x11, 0x66, 0x4E, 0x10, 0xBD, 0xA8, 0xB3, 0x54, 0xC2, 0xC0, 0x39, 0x8D, 0x17, 0x91, 0xDA, 0xE0, 0x21, 0x86,
            0x8A, 0xD3, 0x24, 0x37, 0x4A, 0x10, 0x13, 0x0A, 0x38, 0x45, 0xE2, 0x26, 0xC6, 0x66, 0xC0, 0xDE, 0x73, 0x9B,
            0x53, 0xE2, 0x2D, 0x0A, 0x57, 0x7E, 0xAC, 0xC9, 0xC4, 0x0C, 0x04, 0x33, 0xD5, 0xFA, 0x9F, 0xE5, 0x15, 0x8A,
            0xFD, 0x95, 0xCF, 0x9A, 0x57, 0x16, 0x02, 0xB2, 0x81, 0xBE, 0x39, 0x8C, 0x3A, 0x72, 0x6A, 0x6F, 0x34, 0x8A,
            0x2F, 0x84, 0x0E, 0xEE, 0x96, 0x6D, 0x80, 0x83, 0xBC, 0x6A, 0x02, 0x45, 0x84, 0x3A, 0x1C, 0x49, 0xA0, 0x01,
            0xB7, 0xDA, 0x2C, 0x76, 0x96, 0xFF, 0x1D, 0x8E, 0x49, 0xA7, 0xCA, 0xF5, 0xD6, 0xB0, 0xBD, 0x7F, 0x51, 0x21,
            0x25, 0xEA, 0xAC, 0xB7, 0x15, 0x16, 0xF6, 0x24, 0xD7, 0x0E, 0x54, 0x27, 0x96, 0x0D, 0xEC, 0xD4, 0x96, 0xC9,
            0x00, 0x33, 0x4D, 0x43, 0x83, 0x8C, 0x7B, 0x59, 0x5E, 0x96, 0xAF, 0x5F, 0xAC, 0xC3, 0x4A, 0xF9, 0x23, 0xFC,
            0x62, 0x7B, 0xFF, 0xF5, 0xB9, 0x0C, 0x91, 0x6A, 0x01, 0xCD, 0xC9, 0x87, 0xBB, 0x43, 0xFC, 0xA4, 0xE7, 0x49,
            0x0D, 0xB5, 0xC7, 0xC3, 0x5A, 0x95, 0xF7, 0x52, 0x91, 0x78, 0x1D, 0x52, 0xC4, 0xBC, 0x63, 0x5A, 0xE4, 0x6A,
            0x11, 0x7B, 0xFF, 0x8D, 0x72, 0x8E, 0x64, 0xB5, 0x53, 0xB8, 0x07, 0xDD, 0x4E, 0x7F, 0x4D, 0xF4, 0x35, 0x99,
            0x96, 0x4A, 0xC6, 0xC6, 0xB7, 0x20, 0xF6, 0xEB, 0xA9, 0xA1, 0x18, 0xAF, 0xA7, 0x77, 0x07, 0xE2, 0x0B, 0x49,
            0xBA, 0xE1, 0x12, 0x60, 0x55, 0x41, 0xDD, 0xA8, 0x21, 0x03, 0xE5, 0x5B, 0x8F, 0x81, 0x1E, 0x8D, 0x8B, 0x6A,
            0x11, 0xE0, 0x6F, 0xF9, 0x2F, 0x96, 0xC1, 0xBA, 0x8E, 0x4D, 0x06, 0x06, 0x62, 0x9A, 0xE8, 0x92, 0x66, 0xCC,
            0xFB, 0x34, 0x7B, 0x11, 0x42, 0x34, 0xBC, 0x3D, 0xDC, 0x63, 0x3E, 0x7A, 0xF7, 0x2C, 0xD4, 0x19, 0x60, 0xF5,
            0xF3, 0xC5, 0xE1, 0xF9, 0x1D, 0x5F, 0xB4, 0xEF, 0xEF, 0xBA, 0x4E, 0xB1, 0x35, 0x7B, 0xBD, 0x26, 0x1D, 0x61,
            0xD0, 0xB0, 0xF4, 0x2C, 0x65, 0x64, 0x84, 0x6B, 0xFB, 0x3C, 0x74, 0x6D, 0xE1, 0x93, 0xD2, 0x98, 0x36, 0x2A,
            0x18, 0x5F, 0xFA, 0xE2, 0xE1, 0x23, 0x7C, 0x8C, 0x93, 0x2E, 0x53, 0xEE, 0x40, 0x23, 0x2C, 0x56, 0xF3, 0xFB,
            0xB3, 0xEC, 0xBC, 0xFA, 0xC7, 0x06, 0xA6, 0xC0, 0x4B, 0xCC, 0xE8, 0xBB, 0xC1, 0x4C, 0x84, 0x41, 0x01, 0x67,
            0xA2, 0x8F, 0x43, 0xB2, 0xD6, 0xEA, 0xB6, 0xA4, 0xA0, 0x21, 0xF7, 0x45, 0x5E, 0xBC, 0x8E, 0x9F, 0xF2, 0x03,
            0xCC, 0x3B, 0x5F, 0x35, 0x36, 0xD4, 0x91, 0x18, 0xC3, 0x9E, 0xA6, 0x36, 0x32, 0x44, 0xE0, 0xFA, 0xB2, 0xF1,
            0x91, 0xEF, 0x1F, 0x9D, 0x39, 0x66, 0x10, 0xDA, 0x18, 0xC2, 0xFE, 0x66, 0x73, 0x9F, 0xBA, 0xC8, 0xD2, 0x2C,
            0x7B, 0x23, 0x6A, 0xD9, 0xBD, 0x9E, 0x02, 0xB2, 0x35, 0x7E, 0x87, 0x9E, 0x1B, 0x58, 0x9A, 0xC1, 0x06, 0x70,
            0x49, 0x3D, 0x9A, 0xB4, 0x46, 0x9F, 0x4D, 0x67, 0xCB, 0x2A, 0x82, 0xDC, 0x75, 0x4A, 0x32, 0x70, 0x50, 0x68,
            0x6E, 0x0A, 0x5C, 0x65, 0xF2, 0x5E, 0xC4, 0xF6, 0x0E, 0x34, 0x04, 0x23, 0x24, 0xF3, 0x4B, 0x30, 0xF3, 0xB2,
            0x4E, 0x26, 0x02, 0x07, 0xC8, 0x3D, 0x54, 0xE5, 0xFB, 0x6F, 0xB4, 0xB0, 0x5E, 0x71, 0xD8, 0xE1, 0xB9, 0x44,
            0x92, 0x69, 0x02, 0xBB, 0x5C, 0x16, 0x24, 0x16, 0x70, 0x3E, 0xFD, 0x09, 0xBD, 0xF2, 0xD2, 0x69, 0xE7, 0xEE,
            0x74, 0xB3, 0xA1, 0x92, 0x5A, 0xC0, 0x99, 0x1A, 0xF2, 0xDD, 0x3A, 0x62, 0x5E, 0x81, 0x7D, 0x66, 0xF0, 0xE9,
            0x14, 0xCA, 0x8F, 0xDD, 0x24, 0xA6, 0x5A, 0xD4, 0xD8, 0xD3, 0xB8, 0xBB, 0x03, 0x03, 0x1D, 0xA6, 0x19, 0xD1,
            0xC6, 0x9E, 0xBA, 0x25, 0xA8, 0xD8, 0x16, 0x0B, 0xCF, 0x8D, 0x5C, 0x5B, 0x78, 0xB9, 0x88, 0x60, 0x19, 0xFB,
            0xB8, 0xC1, 0xA0, 0xD9, 0x65, 0xF3, 0x24, 0xAF, 0x9F, 0x6A, 0x4F, 0x72, 0xAC, 0xD2, 0xB3, 0xAC, 0x2F, 0x87,
            0x5C, 0xCB, 0x2B, 0x9A, 0xD0, 0x1C, 0x18, 0x8F, 0xC7, 0xA7, 0x47, 0x26, 0xD6, 0x32, 0xE5, 0x68, 0x4A, 0xA5,
            0xC4, 0x31, 0x7C, 0x16, 0x44, 0x8C, 0xD8, 0xB0, 0x8C, 0x01, 0xD6, 0xCD, 0x51, 0x37, 0x2B, 0x62, 0x7B, 0x0F,
            0x66, 0x20, 0xD8, 0x88, 0x4B, 0x6C, 0x23, 0xAB, 0x1C, 0x84, 0xA2, 0xAF, 0x15, 0x01, 0x95, 0xAC, 0x62, 0x03,
            0xBB, 0x0F, 0xC2, 0x3C, 0x29, 0x0F, 0x24, 0x22, 0xB9, 0x6B, 0x72, 0x86, 0x46, 0xA6, 0xD6, 0xCB, 0x06, 0x0E,
            0xB0, 0x04, 0x2C, 0xBD, 0x7E, 0x35, 0x29, 0xED, 0xFE, 0xF9, 0xB9, 0xC1, 0xBC, 0xC9, 0x0A, 0xD8, 0x5B, 0x2F,
            0x33, 0xE9, 0xD0, 0x0F, 0x3E, 0x9A, 0xCC, 0x63, 0x0C, 0xE0, 0xA3, 0x91, 0x4A, 0x25, 0xE1, 0xA9, 0xB3, 0x6B,
            0xD2, 0xC6, 0xF2, 0xBA, 0x41, 0xD5, 0x51, 0x0F, 0xAE, 0xFB, 0x7C, 0x0F, 0x30, 0xE4, 0x9A, 0xBE, 0x50, 0x36,
            0xF9, 0x7A, 0x17, 0x62, 0x8E, 0x7B, 0x94, 0x23, 0x8C, 0x15, 0x0C, 0xD5, 0x48, 0x02, 0x2B, 0xFB, 0xB6, 0xEB,
            0x5B, 0x22, 0xBE, 0x75, 0x9E, 0x6A, 0x99, 0x1A, 0x0D, 0xF6, 0x90, 0xFC, 0x57, 0x79, 0x43, 0x01, 0x6F, 0x2F,
            0xCD, 0x74, 0xAB, 0x74, 0xF5, 0x65, 0x9D, 0x43, 0xBB, 0x13, 0xDE, 0xD5, 0x6D, 0x97, 0x08, 0xA9, 0x9E, 0x11,
            0x2E, 0x2A, 0x29, 0xA0, 0xFD, 0x3F, 0x84, 0x52, 0xDB, 0xFB, 0xB4, 0x67, 0x30, 0xB3, 0x08, 0x0B, 0x2D, 0xB7,
            0xEE, 0xDA, 0x41, 0xED, 0x1C, 0x6A, 0x7F, 0x98, 0x4F, 0x14, 0x45, 0x75, 0xD4, 0x42, 0x44, 0x8C, 0x34, 0x86,
            0x4F, 0xD9, 0x28, 0xAF, 0x10, 0x1E, 0x25, 0x22, 0xF7, 0x1A, 0xC0, 0xBE, 0xA0, 0x5D, 0x1E, 0x7C, 0xE3, 0x0F,
            0xBE, 0x17, 0xE4, 0xC5, 0xD5, 0xF9, 0x4D, 0xD0, 0x7F, 0xA7, 0xF0, 0xF0, 0x9D, 0x09, 0x0A, 0x66, 0xAD, 0x6A,
            0x85, 0x1D, 0xFD, 0x3F, 0x51
        };

        private static readonly byte[] MetaDataAesKey = {0xE3, 0x05, 0x62, 0x14, 0xD6, 0x0A, 0x20, 0x25, 0x36, 0x96, 0x1B, 0x07, 0x74, 0xDC, 0x24, 0x02};
        private static readonly byte[] MetaDataAesIV = {0x1D, 0x6E, 0xEB, 0x4C, 0x86, 0xA9, 0x45, 0x44, 0x45, 0x72, 0x12, 0x21, 0x2B, 0x43, 0x25, 0x2F};

        private static readonly byte[] BlockInfoSM4Key = {0x02, 0x24, 0xDC, 0x74, 0x07, 0x1B, 0x94, 0x36, 0x25, 0x20, 0x0A, 0xD6, 0x14, 0x62, 0x05, 0xE3};
        private static readonly byte[] BlockInfoSM4IV = {0x79, 0x7B, 0xCD, 0x5D, 0x7D, 0x7B, 0xB1, 0x11, 0x43, 0xD0, 0x0D, 0x71, 0x3C, 0xDA, 0xA8, 0x08};

        private static readonly SM4 _sm4 = new (BlockInfoSM4Key);

        private static void Decrypt(byte[] data, int offset, int size, byte[] key)
        {
            for (var i = 0; i < size; ++i)
            {
                data[offset + i] = (byte)((data[offset + i] ^ ~key[i % key.Length]) + 0x49);
            }
        }

        public static void DecryptAssetBundleSize(byte[] bundle, int offset)
        {
            Decrypt(bundle, offset, MetaDataKey.Length, MetaDataKey);
        }

        public static void DecryptAssetBundleSizeAes(byte[] bundle, int offset)
        {
            var metaBlockEnc = new byte[0x10];
            BitConverter.GetBytes(BigEndian.ToUInt64(bundle, offset + 0)).CopyTo(metaBlockEnc, 0);
            BitConverter.GetBytes(BigEndian.ToUInt32(bundle, offset + 8)).CopyTo(metaBlockEnc, 8);
            BitConverter.GetBytes(BigEndian.ToUInt32(bundle, offset + 12)).CopyTo(metaBlockEnc, 12);

            using var aes = new AesCryptoServiceProvider {Key = MetaDataAesKey, IV = MetaDataAesIV, Padding = PaddingMode.None, Mode = CipherMode.CBC};


            using var ms = new MemoryStream(metaBlockEnc, 0, metaBlockEnc.Length);
            using var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read);

            cs.Read(bundle, offset, metaBlockEnc.Length);

            BitConverter.GetBytes(BigEndian.ToUInt64(bundle, offset + 0)).CopyTo(bundle, offset + 0);
            BitConverter.GetBytes(BigEndian.ToUInt32(bundle, offset + 8)).CopyTo(bundle, offset + 8);
            BitConverter.GetBytes(BigEndian.ToUInt32(bundle, offset + 12)).CopyTo(bundle, offset + 12);
        }

        public static void DecryptAssetBundleCompressedBlockInfo(byte[] bundle, int offset, int size)
        {
            Decrypt(bundle, offset, size, BlockInfoKey);
        }

        public static void DecryptAssetBundleCompressedBlockInfoSM4(byte[] bundle, int offset, int size)
        {
            _sm4.DecryptCbc(BlockInfoSM4IV, bundle, offset, size);
        }

        public static byte[] FixAssetBundleBlockInfo(byte[] blockInfo)
        {
            var blockInfoCount = BigEndian.ToInt32(blockInfo, 0x10);
            var directoryCount = BigEndian.ToInt32(blockInfo, 0x14 + 0xC * blockInfoCount);


            // Copy the fixed header
            var fixedInfo = new byte[blockInfo.Length - 2 * blockInfoCount];
            Buffer.BlockCopy(blockInfo, 0, fixedInfo, 0, 0x14);

            // Copy fixed block infos
            for (var i = 0; i < blockInfoCount; ++i)
            {
                var blockOffset = 0x14 + 0xC * i;
                var fixedOffset = 0x14 + 0xA * i;

                // Copy Uncompressed Size
                Buffer.BlockCopy(blockInfo, blockOffset + 8, fixedInfo, fixedOffset + 0, 4);

                // Copy Compressed Size
                Buffer.BlockCopy(blockInfo, blockOffset + 4, fixedInfo, fixedOffset + 4, 4);

                // Copy Flags
                Buffer.BlockCopy(blockInfo, blockOffset + 0, fixedInfo, fixedOffset + 8, 2);
            }

            // Copy Directory Nodes
            Buffer.BlockCopy(blockInfo, 0x14 + 0xC * blockInfoCount, fixedInfo, 0x14 + 0xA * blockInfoCount, blockInfo.Length - (0x14 + 0xC * blockInfoCount));

            // Fix Directory Nodes
            var curOfs = 0x14 + 0xA * blockInfoCount + 4;
            for (var i = 0; i < directoryCount; ++i)
            {
                // Swap offset and size
                var size = BitConverter.ToInt64(fixedInfo, curOfs);
                var offset = BitConverter.ToInt64(fixedInfo, curOfs + 8);
            
                BitConverter.GetBytes(offset).CopyTo(fixedInfo, curOfs);
                BitConverter.GetBytes(size).CopyTo(fixedInfo, curOfs + 8);
            
                // Advance to next directory node
                curOfs = Array.FindIndex(fixedInfo, curOfs + 0x14, b => b == 0) + 1;
            }

            return fixedInfo;
        }
    }
}
