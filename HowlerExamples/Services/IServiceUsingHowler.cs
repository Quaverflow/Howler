using HowlerExamples.Models;

namespace HowlerExamples.Services;

public interface IServiceUsingHowler
{
    string GetData();
    string GetMoreData();
    void PostData(Dto dto);
    Task<string> PostDataAndNotify(DtoNotifiable dto);
}