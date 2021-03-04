using System;
using System.IO;
using System.IO.Compression;
using Newtonsoft.Json;
using PbSerial;
using static UntieUnite.Core.ResDecoder;

namespace UntieUnite.Core
{
    public static class Dumper
    {
        public static void ExtractBins(string outDir, string inDir, bool jsonResMap = true, bool extraData = true)
        {
            Directory.CreateDirectory(outDir);

            var resMapData = GetResMap(inDir);
            File.WriteAllBytes(Path.Combine(outDir, "ResMapPb.pb"), resMapData);

            if (ProtoTableDumper.GetProtoData(typeof(PbResMap), resMapData) is not PbResMap resmap)
                throw new NullReferenceException($"Failed to decode the {nameof(PbResMap)} object");

            if (jsonResMap)
                ExportResMapJson(outDir, resmap);

            if (extraData)
                DumpExtra(outDir, inDir, resmap);
        }

        private static byte[] GetResMap(string inDir)
        {
            var resMapPath = Path.Combine(inDir, "ResMapPb.bytes");
            var resMapRaw = File.ReadAllBytes(resMapPath);
            return DecryptAndDecompress(0xC093D547, resMapRaw);
        }

        private static byte[] ReadZipEntry(ZipArchiveEntry entry)
        {
            var data = new byte[entry.Length];
            using var stream = entry.Open();
            stream.Read(data, 0, data.Length);
            return data;
        }

        private static void ExportResMapJson(string outDir, PbResMap resmap)
        {
            var serialized = JsonConvert.SerializeObject(resmap, Formatting.Indented);
            File.WriteAllText(Path.Combine(outDir, "ResMapPb.json"), serialized);
        }

        private static void DumpExtra(string outDir, string inDir, PbResMap resmap)
        {
            var dirDumpLangMap = Path.Combine(outDir, "LanguageMap");
            var dirDumpDataBin = Path.Combine(outDir, "Databins");
            var dirDumpLua = Path.Combine(outDir, "Lua");

            Directory.CreateDirectory(dirDumpLangMap);
            Directory.CreateDirectory(dirDumpDataBin);
            Directory.CreateDirectory(dirDumpLua);

            var dataBinPath = Path.Combine(inDir, "Databins.zip");
            using var databinZip = ZipFile.OpenRead(dataBinPath);

            foreach (var (hash, name) in resmap.HashToAssetNames)
            {
                if (name.StartsWith("languagemap"))
                {
                    var path = Path.Combine(inDir, "LanguageMap", $"{hash}.bytes");
                    var raw = File.ReadAllBytes(path);
                    var data = DecryptAndDecompress(0x9B1728AF, raw);
                    File.WriteAllBytes(Path.Combine(dirDumpLangMap, name), data);
                }
                else if (name.StartsWith("databin"))
                {
                    var entry = databinZip.GetEntry($"{hash}.bytes");
                    var raw = ReadZipEntry(entry);
                    var data = DecryptAndDecompress(0x9B1728AF, raw);
                    File.WriteAllBytes(Path.Combine(dirDumpDataBin, name), data);
                }
                else if (name.StartsWith("lua"))
                {
                    foreach (var node in resmap.ResNodeList)
                    {
                        if (node.AssetNameHash != hash)
                            continue;

                        var ep = resmap.ExtracPathStrPool[node.ExtractionPathId];
                        var split = ep.Split("/");
                        var relativePath = $"{split[^1]}.zip";
                        var path = Path.Combine(inDir, relativePath);
                        using var luaZip = ZipFile.OpenRead(path);

                        var entry = luaZip.GetEntry($"{hash}.bytes");
                        var raw = ReadZipEntry(entry);
                        var data = Decrypt(0xC0F7D582, raw);
                        File.WriteAllBytes(Path.Combine(dirDumpLua, name), data);
                        break;
                    }
                }
            }
        }
    }
}
