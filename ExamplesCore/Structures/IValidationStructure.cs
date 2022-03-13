using ExamplesCore.Structures.StructureDtos;

namespace ExamplesCore.Structures;

public interface IValidationStructure
{
    Task ValidateAsync(IValidationStructureData dto);
}