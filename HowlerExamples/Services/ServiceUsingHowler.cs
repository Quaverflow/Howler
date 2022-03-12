using Howler;
using HowlerExamples.Helpers;
using HowlerExamples.Models;
using HowlerExamples.Structures;

namespace HowlerExamples.Services;

public class ServiceUsingHowler : IServiceUsingHowler
{
    public string GetData() => "Hello!";
    public string GetMoreData() => "GoodBye!";
    public string PostData(Dto dto) => dto.ToJson();
    public string PostDataGenerics(DtoGeneric dto) => dto.ToJson();
}
