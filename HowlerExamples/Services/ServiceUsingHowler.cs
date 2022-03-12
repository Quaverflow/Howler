using Howler;
using HowlerExamples.Helpers;
using HowlerExamples.Models;
using HowlerExamples.Structures;

namespace HowlerExamples.Services;

public class ServiceUsingHowler : IServiceUsingHowler
{
    public string GetData() => "Hello!";
    public string GetMoreData() => "GoodBye!";
    public void PostData(Dto dto) => dto.ToJson();
    public void PostDataAndNotify(DtoNotifiable dto) => dto.ToJson();
}
