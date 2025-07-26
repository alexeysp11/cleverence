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

        // Add the last character into the result.
        if (previousChar == input.Last())
        {
            result.Append(previousChar);
            if (charRepeatNum > 1)
            {
                result.Append(charRepeatNum);
            }
        }

        return result.ToString();
    }

    public static string Decompress(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return "";
        }
        
        var result = new StringBuilder();

        char? previousChar = null;
        foreach (char ch in input)
        {
            int parsedValue = 0;

            // Initialize the first char.
            if (previousChar == null)
            {
                if (int.TryParse(ch.ToString(), out parsedValue))
                {
                    throw new Exception("First character could not be integer");
                }
                previousChar = ch;
                continue;
            }

            // Append the previous char multiple times.
            if (int.TryParse(ch.ToString(), out parsedValue))
            {
                result.Append(new string(previousChar.Value, parsedValue));
                previousChar = null;
                continue;
            }

            // If the previous char is followed by the next one, append the previous char.
            if (previousChar != ch)
            {
                result.Append(previousChar);
                previousChar = ch;
                continue;
            }
        }

        return result.ToString();
    }
}
