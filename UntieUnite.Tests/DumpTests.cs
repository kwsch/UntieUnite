using System.Diagnostics;
using System.IO;
using FluentAssertions;
using Newtonsoft.Json;
using UntieUnite.Core;
using Xunit;

namespace UntieUnite.Tests
{
    public class DumpTests
    {
        [Theory]
        [InlineData(@"D:\roms\jp.pokemon.pokemonunite")]
        public void FileNameHashing(string dir)
        {
            var rmpb = Directory.GetFiles(dir, "ResMapPb.bytes", SearchOption.AllDirectories);
            rmpb.Length.Should().Be(1);

            var fn = rmpb[0];
            var decoder = new ResDecoder(0xC093D547);
            var encrypted = File.ReadAllBytes(fn);
            var result = decoder.TryDecryptBytes(encrypted, out var decrypted);
            result.Should().BeTrue();
            File.WriteAllBytes("decrypt.bin", decrypted);

            Directory.CreateDirectory("rawProto");
            var dec = FileUtil.DecompressZlib(decrypted);
            File.WriteAllBytes(Path.Combine("rawProto", "PbResMap.pb"), dec);
        }

        [Theory]
        [InlineData("")]
        public void Dump(string outRoot)
        {
            DumpProto(outRoot);
        }

        private static void DumpProto(string outRoot)
        {
            var pdf = Path.Combine(outRoot, "protodump");
            Directory.CreateDirectory(pdf);

            var types = ProtoTableDumper.GetProtoTypes();
            foreach (var t in types)
            {
                var name = t.Name;
                var filename = $"{name}.pb";
                var path = Path.Combine(outRoot, "rawProto", filename);
                if (!File.Exists(path))
                {
                    Debug.WriteLine($"Couldn't find proto data file: {name}");
                    continue;
                }

                var data = File.ReadAllBytes(path);
                var proto = ProtoTableDumper.GetProtoData(t, data);
                var text = JsonConvert.SerializeObject(proto, Formatting.Indented);
                var outpath = Path.Combine(pdf, $"{name}.json");
                File.WriteAllText(outpath, text);
            }
        }
    }
}
