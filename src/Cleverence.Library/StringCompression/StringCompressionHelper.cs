using System.Linq;
using System.Text;

namespace Cleverence.Library.StringCompression;

public static class StringCompressionHelper
{
    /// <summary>
    /// Compress the input string.
    /// </summary>
    public static string Compress(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return "";
        }

        var result = new StringBuilder();

        char? previousChar = null;
        int charRepeatNum = 0;
        foreach (char ch in input)
        {
            // Initialize the first char.
            if (previousChar == null)
            {
                previousChar = ch;
                charRepeatNum = 1;
                continue;
            }

            // Increment if previous char is equal to the current char.
            if (previousChar == ch)
            {
                charRepeatNum += 1;
                continue;
            }

            // Append previous char.
            result.Append(previousChar);
            if (charRepeatNum > 1)
            {
                result.Append(charRepeatNum);
            }

            // Save current char.
            previousChar = ch;
            charRepeatNum = 1;
        }
        return result.ToString();
    }

    public static string Decompress(string input)
    {
        return "";
    }
}
