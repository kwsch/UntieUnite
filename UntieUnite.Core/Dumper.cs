using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using Newtonsoft.Json;
using PbSerial;
using static UntieUnite.Core.ResDecoder;

namespace UntieUnite.Core
{
    /// <summary>
    /// Catch-all for dumping data from a DLC folder into a result path.
    /// </summary>
    public static class Dumper
    {
        /// <summary>
        /// Dumps the raw DLC data assets.
        /// </summary>
        /// <param name="inDir">DLC_0 path</param>
        /// <param name="outDir">Directory to dump results in</param>
        /// <param name="jsonResMap">Option to save a json of the ResourceMap proto</param>
        /// <param name="extraData">Option to dump extra data (<see cref="DumpExtra"/>)</param>
        public static void ExtractBins(string inDir, string outDir, bool jsonResMap = true, bool extraData = true)
        {
            Directory.CreateDirectory(outDir);

            var resMapData = GetResMap(inDir);
            File.WriteAllBytes(Path.Combine(outDir, "ResMapPb.pb"), resMapData);

            if (ProtoTableDumper.GetProtoData(typeof(PbResMap), resMapData) is not PbResMap resmap)
                throw new NullReferenceException($"Failed to decode the {nameof(PbResMap)} object");

            if (jsonResMap)
                ExportResMapJson(outDir, resmap);

            if (extraData)
                DumpExtra(inDir, outDir, resmap);
        }

        private static byte[] GetResMap(string inDir)
        {
            var resMapPath = Path.Combine(inDir, "ResMapPb.bytes");
            var resMapRaw = File.ReadAllBytes(resMapPath);
            return DecryptAndDecompress(EncryptKey._0xC093D547, resMapRaw);
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

        /// <summary>
        /// Dumps all raw protobuf data that can be converted into proto classes.
        /// </summary>
        /// <param name="inDir">Directory that contains the raw DLC assets</param>
        /// <param name="outDir">Directory to write the result to.</param>
        /// <param name="resmap">Resource Map object instance</param>
        private static void DumpExtra(string inDir, string outDir, PbResMap resmap)
        {
            var dirDumpLangMap = Path.Combine(outDir, "LanguageMap");
            var dirDumpDataBin = Path.Combine(outDir, "Databins");
            var dirDumpLua = Path.Combine(outDir, "Lua");
            var dirDumpAssetBundle = Path.Combine(outDir, "AssetBundles");

            Directory.CreateDirectory(dirDumpLangMap);
            Directory.CreateDirectory(dirDumpDataBin);
            Directory.CreateDirectory(dirDumpLua);
            Directory.CreateDirectory(dirDumpAssetBundle);

            var dataBinPath = Path.Combine(inDir, "Databins.zip");
            using var databinZip = ZipFile.OpenRead(dataBinPath);

            foreach (var (hash, name) in resmap.HashToAssetNames)
            {
                if (name.StartsWith("languagemap"))
                {
                    var path = Path.Combine(inDir, "LanguageMap", $"{hash}.bytes");
                    var raw = File.ReadAllBytes(path);
                    var data = DecryptAndDecompress(EncryptKey._0x9B1728AF, raw);
                    File.WriteAllBytes(Path.Combine(dirDumpLangMap, name), data);
                }
                else if (name.StartsWith("databin"))
                {
                    var entry = databinZip.GetEntry($"{hash}.bytes");
                    var raw = ReadZipEntry(entry);
                    var data = DecryptAndDecompress(EncryptKey._0x9B1728AF, raw);
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
                        var data = Decrypt(EncryptKey._0xC0F7D582, raw);
                        File.WriteAllBytes(Path.Combine(dirDumpLua, name), data);
                        break;
                    }
                }
            }

            foreach (var bundleInfo in resmap.Assetbundles)
            {
                var bundle = File.ReadAllBytes(Path.Combine(inDir, bundleInfo.Name));

                DecryptAssetBundle(bundle);

                File.WriteAllBytes(Path.Combine(dirDumpAssetBundle, bundleInfo.Name), bundle);
            }
        }

        /// <summary>
        /// Dumps all raw protobuf data that can be converted into proto classes.
        /// </summary>
        /// <param name="inDir">Directory that contains the raw protobuf data</param>
        /// <param name="outDir">Directory to write the result to.</param>
        public static void DumpAllProtoData(string inDir, string outDir)
        {
            Directory.CreateDirectory(outDir);

            var types = ProtoTableDumper.GetProtoTypes();
            foreach (var t in types)
                DumpProto(inDir, outDir, t);
        }

        /// <summary>
        /// Dumps a <see cref="pbType"/>'s raw data to json.
        /// </summary>
        /// <param name="inDir">Directory that contains the raw protobuf data</param>
        /// <param name="outDir">Directory to write the result to.</param>
        /// <param name="pbType">Type of protobuf to dump (looks for a matching name file with *.pb extension).</param>
        public static void DumpProto(string inDir, string outDir, Type pbType)
        {
            var name = pbType.Name;
            var filename = $"{name}.pb";
            var path = Path.Combine(inDir, filename);
            if (!File.Exists(path))
            {
                Console.WriteLine($"Couldn't find proto data file: {name}");
                return;
            }

            var data = File.ReadAllBytes(path);
            var proto = ProtoTableDumper.GetProtoData(pbType, data);

            var json = JsonConvert.SerializeObject(proto, Formatting.Indented);
            var jsonPath = Path.Combine(outDir, $"{name}.json");
            File.WriteAllText(jsonPath, json);
        }

        /// <summary>
        /// Dumps the global metadata strings from the <see cref="inDirApk"/> root path.
        /// </summary>
        /// <param name="inDirApk">Root path of an unzipped APK file.</param>
        /// <param name="outDir">Path to dump the lines.</param>
        public static void DumpGlobalMetadataStrings(string inDirApk, string outDir)
        {
            // root\assets\bin\Data\Managed\Metadata\global.metadata.dat
            var path = Directory.GetFiles(inDirApk, "global-metadata.dat", SearchOption.AllDirectories);
            var data = File.ReadAllBytes(path[0]);
            var gmeta = new GlobalMetadata(data);
            var strings = gmeta.GetEntries();

            var outPath = Path.Combine(outDir, "global-metadata.txt");
            File.WriteAllLines(outPath, strings);
        }

        /// <summary>
        /// Decrypts a Unity Asset Bundle (*.bundle)
        /// </summary>
        /// <param name="bundle">Raw *.bundle file</param>
        /// <remarks>Early returns if it is not encrypted.</remarks>
        public static void DecryptAssetBundle(byte[] bundle)
        {
            var signatureLen = Array.IndexOf(bundle, (byte)0, 0);
            if (Encoding.UTF8.GetString(bundle, 0, signatureLen) != "UnityFS")
                return;

            var version = BigEndian.ToUInt32(bundle, signatureLen + 1);
            if (version > 6)
                return;

            var unityVersionEnd = Array.IndexOf(bundle, (byte)0, signatureLen + 1 + 4);
            var unityRevisionEnd = Array.IndexOf(bundle, (byte)0, unityVersionEnd + 1);

            var offset = unityRevisionEnd + 1;

            // Check that the bundle is actually encrypted.
            var flags = BigEndian.ToUInt32(bundle, offset + 0x10);
            if ((flags & 0x200) == 0)
                return;

            // Decrypt the bundle size.
            AssetCrypto.DecryptAssetBundleSize(bundle, offset);

            // Clear the compressed bit.
            BigEndian.GetBytes(flags & ~0x200u).CopyTo(bundle, offset + 0x10);

            // Decrypt the bundle block.
            AssetCrypto.DecryptAssetBundleCompressedBlockInfo(bundle, offset + 0x14, BigEndian.ToInt32(bundle, offset + 0x8));
        }
    }
}
