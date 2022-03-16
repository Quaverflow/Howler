using ExamplesForWiseUp.CrossCuttingConcerns.Interfaces;
using ExamplesForWiseUp.Helpers;
using ExamplesForWiseUp.Structures.Notifications;

namespace ExamplesForWiseUp.CrossCuttingConcerns.Implementations;

public class FakeSmsSender : IFakeSmsSender
{
    public async Task Send(SmsDto sms) => await Task.Run(()=> FakesRepository.SmsSent.Add(sms));
}