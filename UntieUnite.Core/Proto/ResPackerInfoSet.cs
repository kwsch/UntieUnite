using System;
using System.Runtime.CompilerServices;
using Google.Protobuf;

namespace PbSerial {
    public class ResPackerInfoSet {
        public static class Parser {
            public static PbResMap ParseFrom(byte[] data) {
                PbResMap map = new PbResMap();
                using var stream = new CodedInputStream(data);
                map.Version = "ProtoUnite";
                map.BundleVersionName = stream.ReadString();
                map.BundleVersionCode = stream.ReadInt32();
                map.GcloudResVersion = stream.ReadInt32();
                map.BattleMatchVersion0 = stream.ReadUInt64();
                map.BattleMatchVersion1 = stream.ReadUInt64();
                map.BattleMatchSignatureCode = stream.ReadString();
                map.BattleMatchSignatureRes = stream.ReadString();
                map.BattleMatchSignatureTable = stream.ReadInt32();
                /* Skip Empty Repeated */
                stream.ReadInt32();
                stream.ReadInt32();
                stream.ReadInt32();
                /* Above should all be count 0 */
                int strPoolCount = stream.ReadInt32();
                for (int i = 0; i < strPoolCount; i++) {
                    map.ExtStrPool.Add(i, stream.ReadString());
                }
                int assetBundleCount = stream.ReadInt32();
                for (int i = 0; i < assetBundleCount; i++) {
                    PbAssetBundle bundle = new PbAssetBundle();
                    stream.ReadString(); // Path
                    bundle.Name = stream.ReadString();
                    stream.ReadInt32(); // Size
                    bundle.AbId = stream.ReadInt32();
                    var bc = stream.ReadInt32();
                    bundle.SecurityKey = stream.ReadBytes().ToBase64();
                    for (int j = bc; j > 0; --j) {
                        uint hash = stream.ReadUInt32();
                        stream.ReadUInt32(); // type
                        string ext = map.ExtStrPool[stream.ReadInt32()];
                        stream.ReadUInt32(); // Unk (7)

                        bundle.Content.Add(hash + ext);
                    }
                    map.Assetbundles.Add(bundle);
                }
                return map;
            }
        }
    }
}