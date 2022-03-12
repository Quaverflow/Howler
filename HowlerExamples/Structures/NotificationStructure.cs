using HowlerExamples.Helpers;
using HowlerExamples.Models;

namespace HowlerExamples.Structures;

public class NotificationStructure: INotificationStructure
{
    public void SendEmail(EmailDto email) => FakesRepository.EmailsSent.Add(email);
    public void SendSms(SmsDto sms) => FakesRepository.SmsSent.Add(sms);
    public void SendNotification(NotificationDto data)
    {
        SendEmail(data.Email);
        SendSms(data.Sms);
    }
}