using Howler;
using HowlerExamples.Helpers;
using HowlerExamples.Models;
using HowlerExamples.Structures.Base;

namespace HowlerExamples.Services;

public class ServiceUsingHowler : IServiceUsingHowler
{
    private readonly IHowler _howler;

    public ServiceUsingHowler(IHowler howler)
    {
        _howler = howler;
    }
    public string GetData() => "Hello!";
    public string GetMoreData() => "GoodBye!";
    public void PostData(Dto dto) => dto.ToJson();
    public void PostDataAndNotify(DtoNotifiable dto)
    {

        // do operations with the dto.

        _howler.InvokeVoid(new EmailDto(dto.Email, "This is how you send an email with Je ne sais quoi!", "Sending notifications"), StructuresIds.SendEmail);
        _howler.InvokeVoid(new SmsDto(dto.PhoneNumber, "This is how you send a text with aplomb!"), StructuresIds.SendSms);
    }
}
