namespace HowlerExamples.CrossCuttingConcerns;

public interface IFakeLogger
{
    void Log(string message);
    void Clear();
    IReadOnlyList<string> GetLogs();
}