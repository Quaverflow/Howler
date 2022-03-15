using ExamplesForWiseUp.CrossCuttingConcerns.Interfaces;
using ExamplesForWiseUp.Helpers;

namespace ExamplesForWiseUp.CrossCuttingConcerns.Implementations;

public class FakeLogger : IFakeLogger
{
    public void Log(string message) => FakesRepository.Logs.Add(message);
}