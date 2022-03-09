using HowlerExamples.Models;
using HowlerExamples.Structures;

namespace HowlerExamples.Services;

public interface IServiceUsingHowler
{
    string GetData();
    string GetMoreData();
    Dto PostData(Dto dto);
}