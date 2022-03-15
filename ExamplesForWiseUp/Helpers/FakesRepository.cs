using ExamplesForWiseUp.Models;

namespace ExamplesForWiseUp.Helpers;

public static class FakesRepository
{
    public static List<string> Logs = new();
    public static List<EmailDto> EmailsSent = new();
    public static List<SmsDto> SmsSent = new();
}