using System;
using System.Collections.Generic;

namespace UntieUnite.Core
{
    public static class Info
    {
        public static Dictionary<string, uint> EncryptionKeys = new()
        {
            {"LMNDataOceanMgr", 0xE18626DD},
            {"LMNLanguageMapDataPool", 0x9B1728AF},
            {"TableBinDataCenter", 0x9B1728AF},
            {"EntityActorCfgUtil", 0xC0F7D582},
            {"GResMap", 0xC093D547},
            {"LMNResMap", 0xC0F7D582},
            {"StreamUtil", 0xC0F7D582},
        };
    }

    public static class HashUtil
    {
        public static uint GetHashCode(IEnumerable<char> value)
        {
            uint hash = 0;
            foreach (var c in value)
                hash = (hash * 29) + c;
            return hash;
        }

        public static uint GetHashCode(IEnumerable<byte> data)
        {
            uint hash = 17;
            foreach (byte b in data)
                hash = (hash * 31) + b;
            return hash;
        }

        public static uint GetHashCode(ReadOnlySpan<byte> data)
        {
            uint hash = 17;
            foreach (byte b in data)
                hash = (hash * 31) + b;
            return hash;
        }
    }
}
