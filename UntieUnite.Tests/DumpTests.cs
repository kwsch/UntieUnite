using System.Diagnostics;
using System.IO;
using FluentAssertions;
using UntieUnite.Core;
using Xunit;

namespace UntieUnite.Tests
{
    /// <summary>
    /// Test some logic. Keep any reusable logic in the <see cref="Core"/> project please!
    /// </summary>
    public class DumpTests
    {
        // test folder, lazy bad!
        private const string outRootDir = "";

        [Theory]
        [InlineData(@"D:\roms\jp.pokemon.pokemonunite", outRootDir)]
        public void FileNameHashing(string dlcRoot, string outRoot)
        {
            var rmpb = Directory.GetFiles(dlcRoot, "ResMapPb.bytes", SearchOption.AllDirectories);
            rmpb.Length.Should().Be(1);

            var fn = rmpb[0];
            var decoder = new ResDecoder(EncryptKey._0xC093D547, AssetFormat.Android);
            var encrypted = File.ReadAllBytes(fn);
            var result = decoder.TryDecryptBytes(encrypted, out var decrypted);
            result.Should().BeTrue();

            var decryptedDumpPath = Path.Combine(outRoot, "decrypt.bin");
            File.WriteAllBytes(decryptedDumpPath, decrypted);

            var outDir = Path.Combine(outRoot, "rawProto");
            Directory.CreateDirectory(outDir);

            var decompressed = FileUtil.DecompressZlib(decrypted);
            var path = Path.Combine(outDir, "PbResMap.pb");
            File.WriteAllBytes(path, decompressed);
        }

        [Theory]
        [InlineData(outRootDir)]
        public void DumpProtos(string outRoot)
        {
            var outDir = Path.Combine(outRoot, "protodump");
            var inDir = Path.Combine(outRoot, "rawProto");
            Dumper.DumpAllProtoData(inDir, outDir);
        }

        [Theory]
        [InlineData(outRootDir)]
        public void DumpGmeta(string gmetaPath)
        {
            var path = Path.Combine(gmetaPath, "global-metadata.dat");
            var data = File.ReadAllBytes(path);
            var gm = new GlobalMetadata(data);
            var lines = gm.GetEntries()
            var outPath = Path.Combine(gmetaPath, "global-metadata_strings.txt");
            File.WriteAllLines(outPath, lines);
        }
    }
}
