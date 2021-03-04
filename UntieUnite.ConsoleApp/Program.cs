using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Newtonsoft.Json;
using PbSerial;
using UntieUnite.Core;
using DeflateStream = Ionic.Zlib.DeflateStream;

namespace UntieUnite
{
    class Program
    {

        static byte[] Decrypt(uint key, byte[] data)
        {
            if (data.Length == 0) return new byte[0];

            if (new ResDecoder(key).TryDecryptBytes(data, out var decrypted))
            {
                return decrypted;
            }

            throw new InvalidDataException();
        }

        static byte[] DecryptAndDecompress(uint key, byte[] data)
        {
            return DeflateStream.UncompressBuffer(Decrypt(key, data));
        }

        static byte[] ReadZipEntry(ZipArchiveEntry entry)
        {
            using var stream = entry.Open();

            var data = new byte[entry.Length];
            stream.Read(data, 0, data.Length);
            return data;
        }

        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine($"usage: in_dir out_dir");
                return;
            }

            var inDir = args[0];
            var outDir = args[1];

            if (!Directory.Exists(outDir))
                Directory.CreateDirectory(outDir);

            var resmapBytes = DecryptAndDecompress(0xC093D547, File.ReadAllBytes(Path.Combine(inDir, "ResMapPb.bytes")));
            File.WriteAllBytes(Path.Combine(outDir, "ResMapPb.pb"), resmapBytes);

            var resmap = (PbResMap)ProtoTableDumper.GetProtoData(typeof(PbResMap), resmapBytes);
            File.WriteAllText(Path.Combine(outDir, "ResMapPb.json"), JsonConvert.SerializeObject(resmap, Formatting.Indented));

            if (!Directory.Exists(Path.Combine(outDir, "LanguageMap")))
                Directory.CreateDirectory(Path.Combine(outDir, "LanguageMap"));

            if (!Directory.Exists(Path.Combine(outDir, "Databins")))
                Directory.CreateDirectory(Path.Combine(outDir, "Databins"));

            if (!Directory.Exists(Path.Combine(outDir, "Lua")))
                Directory.CreateDirectory(Path.Combine(outDir, "Lua"));

            using var databinZip = ZipFile.OpenRead(Path.Combine(inDir, "Databins.zip"));

            foreach (var kvp in resmap.HashToAssetNames)
            {
                if (kvp.Value.StartsWith("languagemap"))
                {
                    var langmap = DecryptAndDecompress(0x9B1728AF, File.ReadAllBytes(Path.Combine(inDir, "LanguageMap", $"{kvp.Key}.bytes")));
                    File.WriteAllBytes(Path.Combine(outDir, "LanguageMap", kvp.Value), langmap);
                }
                else if (kvp.Value.StartsWith("databin"))
                {
                    var databin = DecryptAndDecompress(0x9B1728AF, ReadZipEntry(databinZip.GetEntry($"{kvp.Key}.bytes")));
                    File.WriteAllBytes(Path.Combine(outDir, "Databins", kvp.Value), databin);
                }
                else if (kvp.Value.StartsWith("lua"))
                {
                    foreach (var node in resmap.ResNodeList)
                    {
                        if (node.AssetNameHash == kvp.Key)
                        {
                            using var luaZip = ZipFile.OpenRead(Path.Combine(inDir, $"{resmap.ExtracPathStrPool[node.ExtractionPathId].Split("/").Last()}.zip"));

                            var lua = Decrypt(0xC0F7D582, ReadZipEntry(luaZip.GetEntry($"{kvp.Key}.bytes")));
                            File.WriteAllBytes(Path.Combine(outDir, "Lua", kvp.Value), lua);
                            break;
                        }
                    }
                }

            }
        }
    }
}
