using Cleverence.Library.LogStandards;

namespace Cleverence.Library.Tests.LogStandards
{
    public class LogStandardsHelperTest
    {
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Convert_EmptyString_EmptyString(string input)
        {
            string result = LogStandardsHelper.Convert(input);
            Assert.True(string.IsNullOrEmpty(result));
        }
    }
}
