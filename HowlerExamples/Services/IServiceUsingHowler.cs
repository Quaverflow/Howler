using HowlerExamples.Models;
using HowlerExamples.Structures;

namespace HowlerExamples.Services;

public interface IServiceUsingHowler
{
    string GetData();
    string GetMoreData();
    void PostData(Dto dto);
    void PostDataAndNotify(DtoNotifiable dto);
}