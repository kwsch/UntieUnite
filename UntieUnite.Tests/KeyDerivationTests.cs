using System.Linq;
using FluentAssertions;
using UntieUnite.Core;
using Xunit;

namespace UntieUnite.Tests
{
    public class KeyDerivationTests
    {
        /// <summary>
        /// Empty salt yields the original key.
        /// </summary>
        [Theory]
        [InlineData(0)]
        public void DeriveKeyEmptySalt(uint salt)
        {
            var derived = ResDecoder.GenerateDerivedKey(salt);
            derived.SequenceEqual(ResDecoder.MasterKey).Should().BeTrue();
        }

        [Theory]
        [InlineData(12345678, new byte[] { 0xFC, 0x1E, 0xA5, 0x12, 0xC3, 0x3E, 0x77, 0x75, 0xFE, 0x8B, 0x96, 0x60, 0x82, 0x62, 0x1E, 0x55 })]
        public void DeriveKey(uint salt, byte[] actualDerived)
        {
            var derived = ResDecoder.GenerateDerivedKey(salt);
            derived.SequenceEqual(actualDerived).Should().BeTrue();
        }
    }
}
