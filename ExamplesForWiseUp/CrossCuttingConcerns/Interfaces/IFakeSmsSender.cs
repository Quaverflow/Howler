using ExamplesForWiseUp.Structures.Notifications;

namespace ExamplesForWiseUp.CrossCuttingConcerns.Interfaces;

public interface IFakeSmsSender
{
    Task Send(SmsDto sms);

}