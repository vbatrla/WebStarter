namespace VB.WebStarter.Abstractions
{
    public interface ILogger
    {
        void LogWarning(string source, string message);

        void LogError(string source, string message);
    }
}