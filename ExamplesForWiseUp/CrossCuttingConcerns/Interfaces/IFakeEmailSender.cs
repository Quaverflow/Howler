using ExamplesForWiseUp.Models;

namespace ExamplesForWiseUp.CrossCuttingConcerns.Interfaces;

public interface IFakeEmailSender
{
    Task Send(EmailDto email);
}