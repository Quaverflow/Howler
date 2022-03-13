using ExamplesCore.CrossCuttingConcerns;
using ExamplesCore.Models;

namespace ExamplesCore.Structures;

public class NotificationStructure: INotificationStructure
{
    private readonly IFakeSmsSender _smsSender;
    private readonly IFakeEmailSender _emailSender;

    public NotificationStructure(IFakeSmsSender smsSender, IFakeEmailSender emailSender)
    {
        _smsSender = smsSender;
        _emailSender = emailSender;
    }

    public void SendEmail(EmailDto email) => _emailSender.Send(email);
    public void SendSms(SmsDto sms) => _smsSender.Send(sms);

    public void SendNotification(NotificationDto data)
    {
        SendEmail(data.Email);
        SendSms(data.Sms);
    }
}