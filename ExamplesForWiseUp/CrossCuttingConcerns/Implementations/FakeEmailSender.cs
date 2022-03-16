using ExamplesForWiseUp.CrossCuttingConcerns.Interfaces;
using ExamplesForWiseUp.Helpers;
using ExamplesForWiseUp.Structures.Notifications;

namespace ExamplesForWiseUp.CrossCuttingConcerns.Implementations;

public class FakeEmailSender : IFakeEmailSender
{
    public async Task Send(EmailDto email) =>await Task.Run(()=> FakesRepository.EmailsSent.Add(email));
}