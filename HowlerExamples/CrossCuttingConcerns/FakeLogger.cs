namespace HowlerExamples.CrossCuttingConcerns;

public class FakeLogger : IFakeLogger
{
    private readonly List<string> _logs = new();
    public void Log(string message) => _logs.Add(message);
    public void Clear() => _logs.Clear();
    public IReadOnlyList<string> GetLogs() => _logs;
}