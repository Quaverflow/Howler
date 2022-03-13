using HowlerExamples.Models;

namespace HowlerExamples.CrossCuttingConcerns;

public interface IFakeSmsSender
{
    void Send(SmsDto sms);

}