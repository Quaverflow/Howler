using HowlerExamples.Helpers;
using HowlerExamples.Models;

namespace HowlerExamples.CrossCuttingConcerns;

public class FakeSmsSender : IFakeSmsSender
{
    public void Send(SmsDto sms) => FakesRepository.SmsSent.Add(sms);
}