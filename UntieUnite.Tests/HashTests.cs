using FluentAssertions;
using UntieUnite.Core;
using Xunit;

namespace UntieUnite.Tests
{
    /// <summary>
    /// Sound File Name Hashing?
    /// </summary>
    public class HashTests
    {
        [Theory]
        [InlineData("snd_Init.bnk", 0xe7e311ed)]
        public void FileNameHashing(string fileName, uint hash)
        {
            var calc = HashUtil.GetHashCode(fileName);
            calc.Should().Be(hash);
        }
    }
}
