using HowlerExamples.Helpers;
using HowlerExamples.Structures;

namespace HowlerExamples.CrossCuttingConcerns;

public class FakeLogger : IFakeLogger
{
    public void Log(string message) => FakesRepository.Logs.Add(message);
}