using ExamplesForWiseUp.CrossCuttingConcerns.Interfaces;
using ExamplesForWiseUp.Models;
using Howler;

namespace ExamplesForWiseUp.Structures.Notifications;

public class EmailStructure : IHowlerStructure
{
    private readonly IFakeEmailSender _emailSender;

    public EmailStructure(IFakeEmailSender emailSender)
    {
        _emailSender = emailSender;
    }

    public async Task SendEmailAsync(EmailDto email) => await _emailSender.Send(email);


    public void InvokeRegistrations()
    {
        HowlerRegistry.AddStructure(StructureIds.SendEmail, SendEmailAsync);
    }
}