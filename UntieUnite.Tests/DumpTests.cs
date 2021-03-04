using System.IO;
using System.Linq;
using FluentAssertions;
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

            var fn = rmpb.First();
            var decoder = new ResDecoder(0xC093D547);
            var encrypted = File.ReadAllBytes(fn);
            var result = decoder.TryDecryptBytes(encrypted, out var decrypted);
            result.Should().BeTrue();
            File.WriteAllBytes("decrypt.bin", decrypted);

            var dec = FileUtil.DecompressZlib(decrypted);
            File.WriteAllBytes("decompressed.bin", dec);
        }
    }
}
