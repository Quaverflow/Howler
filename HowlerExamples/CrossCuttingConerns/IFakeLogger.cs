namespace HowlerExamples.Services;

public interface IFakeLogger
{
    void Log(string message);
    void Clear();
    IReadOnlyList<string> GetLogs();
}