using System;
using System.IO;

public enum LogLevel
{
    INFO,
    WARNING,
    ERROR
}

public class Logger
{
    private static Logger instance;
    private LogLevel currentLogLevel = LogLevel.INFO;
    private string logFilePath = "log.txt";

    private Logger() { }

    public static Logger GetInstance()
    {
        if (instance == null)
        {
            instance = new Logger();
        }
        return instance;
    }

    public void Log(string message, LogLevel level)
    {
        if (level >= currentLogLevel)
        {
            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                writer.WriteLine($"[{DateTime.Now}] [{level}] {message}");
            }
        }
    }

    public void SetLogLevel(LogLevel level)
    {
        currentLogLevel = level;
    }
}
