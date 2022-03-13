using ExamplesCore.Helpers;
using ExamplesCore.Models;

namespace ExamplesCore.CrossCuttingConcerns;

public class FakeEmailSender : IFakeEmailSender
{
    public void Send(EmailDto email) => FakesRepository.EmailsSent.Add(email);
}