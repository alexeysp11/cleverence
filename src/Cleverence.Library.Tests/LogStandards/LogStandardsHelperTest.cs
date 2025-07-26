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

        [Theory]
        [InlineData("10.03.2025 15:14:49.523 INFORMATION App version: '3.4.0.48729'", "10-03-2025 15:14:49.5230\tINFO\tDEFAULT\tApp version: '3.4.0.48729'")]
        [InlineData("10.03.2025 15:14:49.523 INFORMATION App version: '3.4.0.48729 | TEST TEST TEST'", "10-03-2025 15:14:49.5230\tINFO\tDEFAULT\tApp version: '3.4.0.48729 | TEST TEST TEST'")]
        public void Convert_FirstType_StandardType(string input, string expectedResult)
        {
            string result = LogStandardsHelper.Convert(input);
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("2025-03-10 15:14:51.5882 | INFO | 11 | MobileComputer.GetDeviceId | Device code '@MINDEO-M40-D-410244015546'", "10-03-2025 15:14:51.5882\tINFO\tMobileComputer.GetDeviceId\tDevice code '@MINDEO-M40-D-410244015546'")]
        [InlineData("2025-03-10 15:14:51.5882 | INFO | 11 | MobileComputer.GetDeviceId | Device code '@MINDEO-M40-D-410244015546 | TEST TEST TEST'", "10-03-2025 15:14:51.5882\tINFO\tMobileComputer.GetDeviceId\tDevice code '@MINDEO-M40-D-410244015546 | TEST TEST TEST'")]
        public void Convert_SecondType_StandardType(string input, string expectedResult)
        {
            string result = LogStandardsHelper.Convert(input);
            Assert.Equal(expectedResult, result);
        }
    }
}
