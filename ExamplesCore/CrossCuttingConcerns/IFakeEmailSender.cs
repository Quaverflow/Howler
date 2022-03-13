using ExamplesCore.Models;

namespace ExamplesCore.CrossCuttingConcerns;

public interface IFakeEmailSender
{
    void Send(EmailDto email);
}