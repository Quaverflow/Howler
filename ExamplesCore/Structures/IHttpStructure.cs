using ExamplesCore.Models;
using ExamplesCore.Structures.StructureDtos;

namespace ExamplesCore.Structures;

public interface IHttpStructure
{
    IControllerResponse? GetStructure(Func<IControllerResponse?> method);
    void PostStructure(Action method, object data);
    Task<IControllerResponse?> PostNotifiableStructure(Func<Task<IControllerResponse?>> method, DtoNotifiable data);
}