using Howler;

namespace HowlerExamples.Structures;

public interface IHttpStructure
{
    object? GetStructure(Delegate method);
    object? PostStructure(Delegate method, IHowlerData data);
}