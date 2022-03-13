using ExamplesCore.Models;

namespace ExamplesCore.Structures;

public interface IHttpStructure
{
    object? GetStructure(Delegate method);
    object? PostStructure(Delegate method, object data);
    object? PostStructure(Delegate method, DtoNotifiable data);
}