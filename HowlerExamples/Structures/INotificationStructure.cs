using HowlerExamples.Models;

namespace HowlerExamples.Structures;

public interface INotificationStructure
{
    void SendEmail(EmailDto email);
    void SendSms(SmsDto sms);
    void SendNotification(NotificationDto data);
}