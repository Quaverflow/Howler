using ExamplesCore.Models;
using ExamplesCore.Structures.StructureDtos;

namespace ExamplesCore.Structures;

public interface IHttpStructure
{
    object? GetStructure(Delegate method);
    object? PostStructure(Func<object?> method, object data);
    Task<IControllerResponse?> PostNotifiableStructure(Func<Task<IControllerResponse?>> method, DtoNotifiable data);
}