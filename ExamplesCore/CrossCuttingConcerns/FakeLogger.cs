using ExamplesCore.Helpers;

namespace ExamplesCore.CrossCuttingConcerns;

public class FakeLogger : IFakeLogger
{
    public void Log(string message) => FakesRepository.Logs.Add(message);
}