using ExamplesCore.Models;
using ExamplesCore.Structures.StructureDtos;

namespace ExamplesCore.Services;

public interface IServiceUsingHowler
{
    string GetData();
    string GetMoreData();
    void PostData(Dto dto);
    Task<IControllerResponse> PostDataAndNotify(DtoNotifiable dto);
}