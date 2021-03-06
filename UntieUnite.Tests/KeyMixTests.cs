using System.Linq;
using FluentAssertions;
using UntieUnite.Core;
using Xunit;

namespace UntieUnite.Tests
{
    public class KeyMixTests
    {
        [Theory]
        [InlineData(0)]
        public void KeyMixingEmptyKey(uint input)
        {
            var calc = ResDecoder.GetMixedKey(input);
            calc.SequenceEqual(ResDecoder.RawKeys).Should().BeTrue();
        }

        [Theory]
        [InlineData(12345678, new byte[] { 0xFC, 0x1E, 0xA5, 0x12, 0xC3, 0x3E, 0x77, 0x75, 0xFE, 0x8B, 0x96, 0x60, 0x82, 0x62, 0x1E, 0x55 })]
        public void KeyMixing(uint input, byte[] mixed)
        {
            var calc = ResDecoder.GetMixedKey(input);
            calc.SequenceEqual(mixed).Should().BeTrue();
        }
    }
}
