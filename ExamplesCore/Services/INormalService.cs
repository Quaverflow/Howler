using ExamplesCore.Models;

namespace ExamplesCore.Services;

public interface INormalService
{
    string GetData();
    Task<string> PostDataAndNotify(DtoNotifiable dto);

}