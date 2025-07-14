using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Logger
{
    // *** Pray to Mila that this code block can be run without any issues ***
    /// <summary>
    /// Write log messages to a file with different log levels, but need specify the log file path before using it, otherwise it will be crash!!!
    /// Usage:
    /// Logger.Log(LogLevel.Debug, "This is a debug message.");
    /// Logger.Log(LogLevel.Info, "This is an info message.");
    /// Logger.Log(LogLevel.Warning, "This is a warning message.");
    /// Logger.Log(LogLevel.Error, "This is an error message.");
    /// Logger.Log(LogLevel.Fatal, "This is a fatal message.");
    /// Logger.Log(LogLevel.Unknown, "Cannot output a message with level: Unknown!");
    /// </summary>

    public static string logFilePath;

    // Static constructor to initialize the log file path
    static Logger()
    {
        InitializeLogFilePath();
    }

    public static void InitializeLogFilePath()
    {
        // Check if the log file path is already set
        if (File.Exists(logFilePath))
        {
            // Clear the existing log file
            File.WriteAllText(logFilePath, string.Empty);
        }
        else
        {
            // Create the log file path if it doesn't exist
            Directory.CreateDirectory(Path.GetDirectoryName(logFilePath));
            File.Create(logFilePath).Dispose();
        }
    }

    public static void Log(LogLevel logLevel, string message)
    {
        string logLevelShort;
        if (string.IsNullOrEmpty(logFilePath))
        {
            InitializeLogFilePath();
        }
        if (string.IsNullOrEmpty(message))
        {
            return; // Do not log empty messages
        }

        // Determine the short representation of the log level
        // D = Debug, I = Info, W = Warning, E = Error, F = Fatal, U = Unknown
        if (logLevel == LogLevel.Debug)
        {
            logLevelShort = "D";
        }
        else if (logLevel == LogLevel.Info)
        {
            logLevelShort = "I";
        }
        else if (logLevel == LogLevel.Warning)
        {
            logLevelShort = "W";
        }
        else if (logLevel == LogLevel.Error)
        {
            logLevelShort = "E";
        }
        else if (logLevel == LogLevel.Fatal)
        {
            logLevelShort = "F";
        }
        else
        {
            logLevelShort = "U";
            message = $"Cannot output a message with level: {logLevel}!"; // If the log level is Unknown, change the message to indicate that
        }
        // Do not write code like this, because it is a piece of shxt code

        string logMessage = $"[{logLevelShort}][{System.DateTime.Now:yyyy/MM/dd HH:mm:ss}] {message}";
        File.AppendAllText(logFilePath, logMessage + System.Environment.NewLine);
        // ↑ This is the core part of write the log message to the file
    }
}
