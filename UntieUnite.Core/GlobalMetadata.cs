using System;
using System.Text;

namespace UntieUnite.Core
{
    /// <summary>
    /// global-metadata.dat
    /// </summary>
    /// <remarks>Can get some juicy strings from this file!</remarks>
    public class GlobalMetadata
    {
        public readonly byte[] Data;

        public readonly int StringLengthTableStart; // 0x8
        public readonly int StringLengthTableSize; // 0xC

        public readonly int StringDataTableStart; // 0x10
        public readonly int StringDataTableSize; // 0x14

        public GlobalMetadata(byte[] data)
        {
            Data = data;

            StringLengthTableStart = BitConverter.ToInt32(data, 0x08);
            StringLengthTableSize = BitConverter.ToInt32(data, 0x0C);
            StringDataTableStart = BitConverter.ToInt32(data, 0x10);
            StringDataTableSize = BitConverter.ToInt32(data, 0x14);
        }

        public string[] GetEntries()
        {
            int stringCount = StringLengthTableSize / 8;
            var result = new string[stringCount];
            for (int i = 0; i < stringCount; i++)
                result[i] = GetStringAtIndex(i);
            return result;
        }

        private string GetStringAtIndex(int index)
        {
            var ofsLen = StringLengthTableStart + (index * 8);
            var strLen = BitConverter.ToInt32(Data, ofsLen);
            var ofsStr = BitConverter.ToInt32(Data, ofsLen + 4);
            return Encoding.UTF8.GetString(Data, StringDataTableStart + ofsStr, strLen);
        }
    }
}
