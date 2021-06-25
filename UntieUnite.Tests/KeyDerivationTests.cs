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
            var derivedAndroid = ResDecoder.GenerateDerivedKey(salt, AssetFormat.Android);
            derivedAndroid.SequenceEqual(ResDecoder.GetMasterKey(AssetFormat.Android)).Should().BeTrue();

            var derivedSwitch = ResDecoder.GenerateDerivedKey(salt, AssetFormat.Switch);
            derivedSwitch.SequenceEqual(ResDecoder.GetMasterKey(AssetFormat.Switch)).Should().BeTrue();
        }

        [Theory]
        [InlineData(12345678, new byte[] { 0xFC, 0x1E, 0xA5, 0x12, 0xC3, 0x3E, 0x77, 0x75, 0xFE, 0x8B, 0x96, 0x60, 0x82, 0x62, 0x1E, 0x55 })]
        public void DeriveKey(uint salt, byte[] actualDerived)
        {
            var derived = ResDecoder.GenerateDerivedKey(salt, AssetFormat.Android);
            derived.SequenceEqual(actualDerived).Should().BeTrue();
        }

        [Theory]
        [InlineData("snd_Init.bnk", 0x290463D3, new uint[] { 0x90E802D5, 0x766566BA, 0x4F73499D, 0x84968488, 0x00D4B24A, 0xBB4750A1, 0xCDFA48E2, 0x275A757B })]
        public void DeriveSwitchSoundKey(string filename, uint header, uint[] actualKey)
        {
            var key = SoundCrypto.GetSwitchKey(filename, header);
            key.SequenceEqual(actualKey).Should().BeTrue();
        }
    }
}
