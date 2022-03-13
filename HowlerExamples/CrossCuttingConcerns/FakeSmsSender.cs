using HowlerExamples.Helpers;
using HowlerExamples.Models;

namespace HowlerExamples.CrossCuttingConcerns;

public class FakeSmsSender : IFakeSmsSender
{
    private int Count;
    public void Send(SmsDto sms)
    {
        if (Count > 0)
        {
            throw new Exception("count was" + Count);
        }
        Count++;
        FakesRepository.SmsSent.Add(sms);
    }
}