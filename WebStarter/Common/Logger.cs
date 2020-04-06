namespace VB.WebStarter.Common
{
    using System.Diagnostics;
    using Abstractions;

    public class Logger : ILogger
    {
        private static ILogger privateInstance = new Logger();

        public static ILogger Instance
        {
            get
            {
                return privateInstance;
            }

            // Use only in Tests
            internal set
            {
                privateInstance = value;
            }
        }

        public void LogWarning(string source, string message)
        {
            EventLog.WriteEntry(source, message, EventLogEntryType.Warning);
        }

        public void LogError(string source, string message)
        {
            EventLog.WriteEntry(source, message, EventLogEntryType.Error);
        }
    }
}