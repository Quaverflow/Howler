using ExamplesForWiseUp.CrossCuttingConcerns.Interfaces;
using ExamplesForWiseUp.Models;
using Howler;

namespace ExamplesForWiseUp.Structures.Notifications;

public class SmsStructure : IHowlerStructure
{
    private readonly IFakeSmsSender _smsSender;

    public SmsStructure(IFakeSmsSender smsSender)
    {
        _smsSender = smsSender;
    }

    public async Task SendSmsAsync(SmsDto sms) => await _smsSender.Send(sms);


    public void InvokeRegistrations()
    {
        HowlerRegistry.AddStructure(StructureIds.SendSms, SendSmsAsync);
    }
}