using HowlerExamples.Helpers;
using HowlerExamples.Models;

namespace HowlerExamples.CrossCuttingConcerns;

public class FakeEmailSender : IFakeEmailSender
{
    public void Send(EmailDto email) => FakesRepository.EmailsSent.Add(email);
}