using System.Collections.Generic;

namespace UntieUnite.Core
{
    public static class EncryptKey
    {
        public static Dictionary<string, uint> EncryptionKeys = new()
        {
            {"LMNDataOceanMgr", _0xE18626DD},

            {"LMNLanguageMapDataPool", _0x9B1728AF},
            {"TableBinDataCenter", _0x9B1728AF},

            {"GResMap", _0xC093D547},

            {"EntityActorCfgUtil", _0xC0F7D582},
            {"LMNResMap", _0xC0F7D582},
            {"StreamUtil", _0xC0F7D582},
        };

        public const uint _0xE18626DD = 0xE18626DD;
        public const uint _0x9B1728AF = 0x9B1728AF;
        public const uint _0xC093D547 = 0xC093D547;
        public const uint _0xC0F7D582 = 0xC0F7D582;
    }
}
