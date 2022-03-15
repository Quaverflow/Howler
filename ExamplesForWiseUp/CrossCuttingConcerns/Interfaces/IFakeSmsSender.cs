using ExamplesForWiseUp.Models;

namespace ExamplesForWiseUp.CrossCuttingConcerns.Interfaces;

public interface IFakeSmsSender
{
    Task Send(SmsDto sms);

}