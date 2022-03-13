using ExamplesCore.Models;

namespace ExamplesCore.CrossCuttingConcerns;

public interface IFakeSmsSender
{
    void Send(SmsDto sms);

}