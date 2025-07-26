using System.Globalization;
using System.Text;

namespace Cleverence.Library.LogStandards;

public static class LogStandardsHelper
{
    private enum LogLevelFromRecord
    {
        None = 0,
        Debug = 1,
        Information = 2,
        Warning = 3,
        Error = 4
    }

    public static string Convert(string logRecord)
    {
        if (string.IsNullOrEmpty(logRecord))
        {
            return string.Empty;
        }

        // Check if it's the second type.
        List<string> logLevelsType02 = [" | INFO | ", " | WARN | ", " | ERROR | ", " | DEBUG | "];
        DateTime? dateTimeType02 = GetDateTimeFromLogRecord(logRecord, logLevelsType02, "yyyy-MM-dd HH:mm:ss.ffff");
        if (dateTimeType02.HasValue)
        {
            // Get the log level.
            LogLevelFromRecord? logLevel02 = GetLogLevelFromRecord(logRecord, logLevelsType02);
            if (!logLevel02.HasValue)
            {
                throw new Exception("Could not define the log level of the second type");
            }

            // Get the string that is located after " | 11 | ".
            string methodAndMessage02 = GetTheRestOfLogRecord(logRecord, " | 11 | ");

            // Get method name.
            string methodName02 = methodAndMessage02.Split(" | ").First();

            // Get message.
            string message02 = GetTheRestOfLogRecord(logRecord, methodName02 + " | ");

            return GetConvertedRecordLog(
                dateTimeType02.Value,
                logLevel02.Value,
                methodName02,
                message02);
        }

        // Check if it's the first type.
        List<string> logLevelsType01 = [" INFORMATION ", " WARNING ", " ERROR ", " DEBUG "];
        DateTime? dateTimeType01 = GetDateTimeFromLogRecord(logRecord, logLevelsType01, "dd.MM.yyyy HH:mm:ss.fff");
        if (!dateTimeType01.HasValue)
        {
            throw new Exception("Could not determine the type of the log record");
        }

        // Get the log level.
        LogLevelFromRecord? logLevel01 = GetLogLevelFromRecord(logRecord, logLevelsType01);
        if (!logLevel01.HasValue)
        {
            throw new Exception("Could not define the log level of the first type");
        }

        // Get message.
        string message01 = GetTheRestOfLogRecord(logRecord, logLevelsType01);

        return GetConvertedRecordLog(
            dateTimeType01.Value,
            logLevel01.Value,
            "DEFAULT",
            message01);
    }

    private static DateTime? GetDateTimeFromLogRecord(string logRecord, IEnumerable<string> logLevels, string format)
    {
        foreach (var level in logLevels)
        {
            DateTime? result = GetDateTimeFromLogRecord(logRecord, level, format);
            if (result.HasValue)
            {
                return result;
            }
        }
        return null;
    }

    private static DateTime? GetDateTimeFromLogRecord(string logRecord, string logLevel, string format)
    {
        int index = logRecord.IndexOf(logLevel);
        if (index == -1)
        {
            return null;
        }
        string dateTimeString = logRecord.Substring(0, index);
        return DateTime.TryParseExact(dateTimeString, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateTime)
            ? dateTime
            : null;
    }

    private static LogLevelFromRecord? GetLogLevelFromRecord(string logRecord, IEnumerable<string> logLevels)
    {
        foreach(var level in logLevels)
        {
            LogLevelFromRecord? logLevel = GetLogLevelFromRecord(logRecord, level);
            if (logLevel.HasValue)
            {
                return logLevel;
            }
        }
        return null;
    }

    private static LogLevelFromRecord? GetLogLevelFromRecord(string logRecord, string logLevel)
    {
        if (logRecord.Contains(logLevel))
        {
            if (logLevel.ToUpper().Contains("INFO"))
            {
                return LogLevelFromRecord.Information;
            }
            else if (logLevel.ToUpper().Contains("WARN"))
            {
                return LogLevelFromRecord.Warning;
            }
            else if (logLevel.ToUpper().Contains("ERROR"))
            {
                return LogLevelFromRecord.Error;
            }
            else if (logLevel.ToUpper().Contains("DEBUG"))
            {
                return LogLevelFromRecord.Debug;
            }
            else
            {
                return null;
            }
        }
        return null;
    }

    private static string GetTheRestOfLogRecord(string logRecord, IEnumerable<string> lastSymbolsList)
    {
        foreach (string lastSymbols in lastSymbolsList)
        {
            string substring = GetTheRestOfLogRecord(logRecord, lastSymbols);
            if (!string.IsNullOrEmpty(substring))
            {
                return substring;
            }
        }
        return string.Empty;
    }

    private static string GetTheRestOfLogRecord(string logRecord, string lastSymbols)
    {
        int index = logRecord.IndexOf(lastSymbols);
        if (index == -1)
        {
            return string.Empty;
        }
        return logRecord.Substring(index + lastSymbols.Length);
    }

    private static string GetConvertedRecordLog(
        DateTime dateTime,
        LogLevelFromRecord logLevel,
        string methodName,
        string message)
    {
        var result = new StringBuilder();

        result.Append(dateTime.ToString("dd-MM-yyyy HH:mm:ss.ffff"));
        result.Append("\t");
        result.Append(GetConvertedLogLevelString(logLevel));
        result.Append("\t");
        result.Append(methodName);
        result.Append("\t");
        result.Append(message);

        return result.ToString();
    }

    private static string GetConvertedLogLevelString(LogLevelFromRecord logLevel)
    {
        switch (logLevel)
        {
            case LogLevelFromRecord.Debug:
                return "DEBUG";

            case LogLevelFromRecord.None:
            case LogLevelFromRecord.Information:
                return "INFO";

            case LogLevelFromRecord.Warning:
                return "WARN";

            case LogLevelFromRecord.Error:
                return "ERROR";
        }
        throw new Exception($"Could not convert the specified log level to string: {logLevel}");
    }
}
