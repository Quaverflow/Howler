using ExamplesCore.Models;

namespace ExamplesCore.Services;

public interface IServiceUsingHowler
{
    string GetData();
    string GetMoreData();
    void PostData(Dto dto);
    Task<string> PostDataAndNotify(DtoNotifiable dto);
}