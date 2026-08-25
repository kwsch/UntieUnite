using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using K4os.Compression.LZ4;
using Newtonsoft.Json;
using PbSerial;
using static UntieUnite.Core.ResDecoder;

using System.Collections.Generic;
using System.Linq;

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
        /// <param name="resMapPb">Option to save the raw data of the ResourceMap proto</param>
        /// <param name="jsonResMap">Option to save a json of the ResourceMap proto</param>
        /// <param name="extraData">Option to dump extra data (<see cref="DumpExtra"/>)</param>
        /// <param name="sound">Option to dump sound data</param>
        public static void ExtractBins(string inDir, string outDir, bool resMapPb = true, bool jsonResMap = true, bool extraData = true, bool sound = true)
        {
            Directory.CreateDirectory(outDir);

            var resMapName = GetResMapName(inDir);
            var resMapRaw = GetResMapRaw(inDir);
            var resMapFormat = GetAssetFormat(resMapRaw);
            var resMapData = DecryptResMap(resMapRaw, resMapFormat, resMapName);

            PbResMap resmap = resMapName == "ResPackerInfoSet.bin" ?
                              ResPackerInfoSet.Parser.ParseFrom(resMapData) :
                              PbResMap.Parser.ParseFrom(resMapData);

        if (jsonResMap)
                ExportResMapJson(outDir, resmap, resMapFormat);

            if (extraData)
                DumpExtra(inDir, outDir, resmap, resMapFormat);

            if (sound)
                DumpSound(inDir, outDir, resMapFormat);
        }

        private static string GetResMapName(string inDir) {
            var resMapPath = Path.Combine(inDir, "ResPackerInfoSet.bin");
            if (!File.Exists(resMapPath))
                return "ResMapPb.bytes";
            return "ResPackerInfoSet.bin";
        }

        private static byte[] GetResMapRaw(string inDir)
        {
            var resMapPath = Path.Combine(inDir, "ResPackerInfoSet.bin");
            if (!File.Exists(resMapPath))
                resMapPath = Path.Combine(inDir, "ResMapPb.bytes");

            return File.ReadAllBytes(resMapPath);
        }

        private static byte[] DecryptResMap(byte[] resMapRaw, AssetFormat format, string name = "ResMapPb.bytes")
        {
            var salt = format switch
            {
                AssetFormat.Android => EncryptKey._0xC093D547,
                AssetFormat.Switch => GetAssetSalt(name),
                _ => throw new ArgumentOutOfRangeException($"Invalid AssetFormat for {name} ({format})"),
            };

            return DecryptAndDecompress(salt, resMapRaw);
        }

        private static byte[] ReadZipEntry(ZipArchiveEntry entry)
        {
            var data = new byte[entry.Length];
            using var stream = entry.Open();
            stream.Read(data);
            return data;
        }

        private static void ExportResMapJson(string outDir, PbResMap resmap, AssetFormat assetFormat)
        {
            var serialized = JsonConvert.SerializeObject(resmap, Formatting.Indented);
            File.WriteAllText(Path.Combine(outDir, "PbResMap.json"), serialized);
        }

        /// <summary>
        /// Dumps all raw protobuf data that can be converted into proto classes.
        /// </summary>
        /// <param name="inDir">Directory that contains the raw DLC assets</param>
        /// <param name="outDir">Directory to write the result to.</param>
        /// <param name="resmap">Resource Map object instance</param>
        /// <param name="assetFormat">Platform the data originated on</param>
        private static void DumpExtra(string inDir, string outDir, PbResMap resmap, AssetFormat assetFormat)
        {
            var dirDumpLangMap = Path.Combine(outDir, "LanguageMap");
            var dirDumpDataBin = Path.Combine(outDir, "Databins");
            var dirDumpLua = Path.Combine(outDir, "Lua");
            var dirDumpAssetBundle = Path.Combine(outDir, "AssetBundles");

            Directory.CreateDirectory(dirDumpLangMap);
            Directory.CreateDirectory(dirDumpDataBin);
            Directory.CreateDirectory(dirDumpLua);
            Directory.CreateDirectory(dirDumpAssetBundle);

            //var dataBinPath = Path.Combine(inDir, "Databins.zip");
            //using var databinZip = ZipFile.OpenRead(dataBinPath);
            /*
            foreach (var (hash, name) in resmap.HashToAssetNames)
            {
                if (name.StartsWith("languagemap"))
                {
                    var path = Path.Combine(inDir, "LanguageMap", $"{hash}.bytes");
                    var raw = File.ReadAllBytes(path);
                    var data = DecryptAndDecompress(EncryptKey._0x9B1728AF, raw);
                    var dest = Path.Combine(dirDumpLangMap, name);
                    File.WriteAllBytes(dest, data);
                }
                else if (name.StartsWith("databin"))
                {
                    //var entry = databinZip.GetEntry($"{hash}.bytes");
                    //var raw = ReadZipEntry(entry);
                    //var data = DecryptAndDecompress(EncryptKey._0x9B1728AF, raw);
                    //var dest = Path.Combine(dirDumpDataBin, name);
                    //File.WriteAllBytes(dest, data);
                }
                else if (name.StartsWith("lua"))
                {
                    foreach (var node in resmap.ResNodeList)
                    {
                        if (node.AssetNameHash != hash)
                            continue;

                        var ep = resmap.ExtracPathStrPool[node.ExtractionPathId];
                        var lastIndex = ep.LastIndexOf('/');
                        var relativePath = $"{ep[(lastIndex + 1)..]}.zip";
                        var path = Path.Combine(inDir, relativePath);
                        using var luaZip = ZipFile.OpenRead(path);

                        var entry = luaZip.GetEntry($"{hash}.bytes");
                        if (entry is null)
                            throw new NullReferenceException($"{nameof(entry)} from AssetID {node.Id} was invalid.");

                        var raw = ReadZipEntry(entry);
                        var data = Decrypt(EncryptKey._0xC0F7D582, raw);
                        var dest = Path.Combine(dirDumpLua, name);
                        File.WriteAllBytes(dest, data);
                        break;
                    }
                }
            }
            */
            string langzip = "";
            foreach (var bundleInfo in resmap.Assetbundles)
            {
                var fileName = bundleInfo.Name;
                if (!fileName.EndsWith(".bundle")) { // Skip zips
                    if (bundleInfo.Content.Contains("1343719561.msbt"))
                        langzip = bundleInfo.Name;
                    continue; 
                }
                
                var path = Path.Combine(inDir, fileName);
                if (!File.Exists(path)) // Just in case
                    continue;
                var bundle = File.ReadAllBytes(path);

                var decBundle = DecryptAssetBundle(bundle, assetFormat, Convert.FromBase64String(bundleInfo.SecurityKey));

                var dest = Path.Combine(dirDumpAssetBundle, fileName);
                File.WriteAllBytes(dest, decBundle);
            }
            // Crude way for language files
            if (!String.IsNullOrEmpty(langzip)) {
                string mapPath = Path.Combine(inDir, "LanguageMap", "538308141.bytes");
                byte[] langm = ResDecoder.DecryptAndDecompress(2177676813, File.ReadAllBytes(mapPath));
                string str = System.Text.Encoding.Default.GetString(langm);
                string[] lines = str.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                string[] langs = { "X", "CHS", "JPN", "ENG", "KOR", "CHT", "FRA", "DEU", "ESP", "ITA" };
                using var zip = ZipFile.OpenRead(Path.Combine(inDir, langzip));
                if (zip == null) {
                    System.Console.WriteLine("Unable to decrypt language msbt files - archive missing");
                    return;
                }
                foreach (var line in lines) {
                    if (String.IsNullOrEmpty(line))
                        continue;
                    string name = Path.GetFileNameWithoutExtension(line);
                    for (int l = 1; l < langs.Length; l++) { // Skip Null
                        string locname = name + "_" + langs[l];
                        string asset = GetAssetName(name + ((char)l).ToString()) + ".msbt";
                        try {
                            byte[] data = DecryptAndDecompress(GetAssetSalt(name), ReadZipEntry(zip.GetEntry(asset)));
                            File.WriteAllBytes(Path.Combine(dirDumpLangMap, locname + ".msbt"), data);
                        } catch (Exception e) {  }
                    }
                }
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
        /// <param name="bundle_">Raw *.bundle file</param>
        /// <param name="assetFormat">Platform the data originated on</param>
        /// <remarks>Early returns if it is not encrypted.</remarks>
        public static byte[] DecryptAssetBundle(byte[] bundle_, AssetFormat assetFormat,  byte[] securityKey = null)
        {
            var bundle = (byte[]) bundle_.Clone();
            var signatureLen = Array.IndexOf(bundle, (byte)0, 0);
            if (Encoding.UTF8.GetString(bundle, 0, signatureLen) != "UnityFS")
                throw new ArgumentException("Invalid Bundle Signature");

            var version = BigEndian.ToUInt32(bundle, signatureLen + 1);
            if (version > 7)
                throw new ArgumentException("Invalid Bundle Version");

            var unityVersionEnd = Array.IndexOf(bundle, (byte)0, signatureLen + 1 + 4);
            var unityRevisionEnd = Array.IndexOf(bundle, (byte)0, unityVersionEnd + 1);

            var offset = unityRevisionEnd + 1;

            // Check that the bundle is actually encrypted.
            var flags = BigEndian.ToUInt32(bundle, offset + 0x10);
            
            if (securityKey != null && securityKey.Length == 1 && securityKey[0] == 0)
                securityKey = null;
            
            if ((flags & 0x400) != 0) {
                if (securityKey == null) 
                    return bundle;

                flags &= ~0x400u;
            } else if ((flags & 0x200) != 0) {
                flags &= ~0x200u;
            } else {
                return bundle;
            }

            // Decrypt the bundle size.
            switch (assetFormat)
            {
                case AssetFormat.Android:
                    AssetCrypto.DecryptAssetBundleSize(bundle, offset);
                    break;
                case AssetFormat.Switch:
                    AssetCrypto.DecryptAssetBundleSizeAes(bundle, offset, securityKey);
                    break;
            }

            // Clear the compressed bit.
            BigEndian.GetBytes(flags).CopyTo(bundle, offset + 0x10);

            // Decrypt the bundle block.
            var compressedBlockSize = BigEndian.ToInt32(bundle, offset + 0x8);
            var blockSize = BigEndian.ToInt32(bundle, offset + 0xC);

            // Version 7 requires to force align
            var blockOffset = offset + 0x14;
            if (version == 7) 
                blockOffset = (blockOffset - 1 | 15) + 1;

            switch (assetFormat) {
                case AssetFormat.Android:
                    AssetCrypto.DecryptAssetBundleCompressedBlockInfo(bundle, blockOffset, compressedBlockSize);
                    break;
                case AssetFormat.Switch:
                    AssetCrypto.DecryptAssetBundleCompressedBlockInfoSM4(bundle, blockOffset, compressedBlockSize, securityKey);
                    break;
            }

            // If Android, we're done.
            if (assetFormat == AssetFormat.Android)
                return bundle;

            // If switch, we need to fix up the compressed block info.
            var compressedBlockInfo = new byte[compressedBlockSize];
            Buffer.BlockCopy(bundle, blockOffset, compressedBlockInfo, 0, compressedBlockSize);

            byte[] blockInfo;
            switch (flags & 0x3F)
            {
                default:
                    blockInfo = (byte[]) compressedBlockInfo.Clone();
                    break;
                case 1: // LZMA
                    {
                        using var compressedStream = new MemoryStream(compressedBlockInfo);
                        using var uncompressedStream = new MemoryStream(blockSize);
                        var decoder = new SevenZip.Compression.LZMA.Decoder();
                        var properties = new byte[5];
                        compressedStream.Read(properties, 0, 5);
                        decoder.SetDecoderProperties(properties);
                        decoder.Code(compressedStream, uncompressedStream, compressedBlockSize - 5, blockSize, null);

                        blockInfo = uncompressedStream.ToArray();
                    }
                    break;
                case 2: // LZ4
                case 3: // LZ4HC
                    blockInfo = new byte[blockSize];
                    LZ4Codec.Decode(compressedBlockInfo, 0, compressedBlockSize, blockInfo, 0, blockSize);
                    break;
            }

            // Clear the compression type.
            flags &= ~0x3Fu;
            BigEndian.GetBytes(flags).CopyTo(bundle, offset + 0x10);

            // Generate fixed block info
            var fixedBlockInfo = AssetCrypto.FixAssetBundleBlockInfo(blockInfo);

            // Generate fixed asset bundle
            var fixedBundle = new byte[bundle.Length - compressedBlockInfo.Length + fixedBlockInfo.Length];
            Buffer.BlockCopy(bundle, 0, fixedBundle, 0, blockOffset);
            Buffer.BlockCopy(fixedBlockInfo, 0, fixedBundle, blockOffset, fixedBlockInfo.Length);
            Buffer.BlockCopy(bundle, blockOffset + compressedBlockInfo.Length, fixedBundle, blockOffset + fixedBlockInfo.Length, bundle.Length - (blockOffset+ compressedBlockInfo.Length));

            BigEndian.GetBytes((ulong)fixedBundle.Length).CopyTo(fixedBundle, offset);
            BigEndian.GetBytes(fixedBlockInfo.Length).CopyTo(fixedBundle, offset + 8);
            BigEndian.GetBytes(fixedBlockInfo.Length).CopyTo(fixedBundle, offset + 12);

            return fixedBundle;
        }

        private static void DumpSound(string inDir, string outDir, AssetFormat assetFormat)
        {
            var dirDumpSound = Path.Combine(outDir, "Sound");
            Directory.CreateDirectory(dirDumpSound);

            var path = Path.Combine(inDir, "Sound", "Switch");
            if (!Directory.Exists(path))
                path = Path.Combine(inDir, "Sound", "Android");

            if (!Directory.Exists(path))
                System.Console.WriteLine("No Sound directory found");
                return;


            var sourceDir = new DirectoryInfo(path);
            foreach (var fi in sourceDir.GetFiles())
            {
                var archive = File.ReadAllBytes(fi.FullName);
                if (SoundCrypto.IsSoundArchive(archive))
                    File.WriteAllBytes(Path.Combine(dirDumpSound, fi.Name), SoundCrypto.Decrypt(archive));

                if (fi.Name == "oe7e311ed.bin")
                {
                    File.WriteAllBytes(Path.Combine(dirDumpSound, fi.Name), SoundCrypto.DecryptSwitch("snd_Init.bnk", archive));
                }
            }
        }
    }
}
