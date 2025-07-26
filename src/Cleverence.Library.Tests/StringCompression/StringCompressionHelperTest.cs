using Cleverence.Library.StringCompression;

namespace Cleverence.Library.Tests.StringCompression
{
    public class StringCompressionHelperTest
    {
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Compress_EmptyString_EmptyString(string input)
        {
            string result = StringCompressionHelper.Compress(input);
            Assert.True(string.IsNullOrEmpty(result));
        }

        [Theory]
        [InlineData("aaabbcccdde", "a3b2c3d2e")]
        [InlineData("abbcccdde", "ab2c3d2e")]
        [InlineData("abbcccddeee", "ab2c3d2e3")]
        [InlineData("abcde", "abcde")]
        [InlineData("abbcde", "ab2cde")]
        public void Compress_SmallLetters_CompressedString(string input, string expectedResult)
        {
            string result = StringCompressionHelper.Compress(input);
            Assert.Equal(expectedResult, result);
        }
    }
}
