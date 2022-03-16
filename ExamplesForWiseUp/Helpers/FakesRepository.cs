using ExamplesForWiseUp.Models;
using ExamplesForWiseUp.Structures.Notifications;

namespace ExamplesForWiseUp.Helpers;

public static class FakesRepository
{
    public static List<string> Logs = new();
    public static List<EmailDto> EmailsSent = new();
    public static List<SmsDto> SmsSent = new();

    public static void Cleanup()
    {
        FakesRepository.Logs.Clear();
        FakesRepository.EmailsSent.Clear();
        FakesRepository.SmsSent.Clear();
    }
}