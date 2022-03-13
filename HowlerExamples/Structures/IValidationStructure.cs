using HowlerExamples.Structures.StructureDtos;

namespace HowlerExamples.Structures;

public interface IValidationStructure
{
    Task Validate(IValidationStructureData dto);
}