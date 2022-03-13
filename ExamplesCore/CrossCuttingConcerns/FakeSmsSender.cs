using ExamplesCore.Helpers;
using ExamplesCore.Models;

namespace ExamplesCore.CrossCuttingConcerns;

public class FakeSmsSender : IFakeSmsSender
{
    public void Send(SmsDto sms) => FakesRepository.SmsSent.Add(sms);
}