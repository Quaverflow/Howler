using System.Collections.Generic;

namespace Howler.Tests.Objects.StructureExamples;

public class FakeLogger : IFakeLogger
{
    private readonly List<string> _logs = new();
    public void Log(string message) => _logs.Add(message);
    public IReadOnlyList<string> GetLogs() => _logs;
}