namespace HowlerExamples.Structures;

public interface IHttpStructureContainer
{
    object? GetStructure(Delegate method);
    object? PostStructure(Delegate method);
}