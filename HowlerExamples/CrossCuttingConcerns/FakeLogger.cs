using HowlerExamples.Helpers;

namespace HowlerExamples.CrossCuttingConcerns;

public class FakeLogger : IFakeLogger
{
    public void Log(string message) => FakesRepository.Logs.Add(message);
}