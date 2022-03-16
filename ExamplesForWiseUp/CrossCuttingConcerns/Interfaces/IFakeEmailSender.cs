using ExamplesForWiseUp.Models;
using ExamplesForWiseUp.Structures.Notifications;

namespace ExamplesForWiseUp.CrossCuttingConcerns.Interfaces;

public interface IFakeEmailSender
{
    Task Send(EmailDto email);
}