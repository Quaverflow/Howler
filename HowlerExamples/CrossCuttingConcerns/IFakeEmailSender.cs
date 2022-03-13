using HowlerExamples.Models;

namespace HowlerExamples.CrossCuttingConcerns;

public interface IFakeEmailSender
{
    void Send(EmailDto email);
}