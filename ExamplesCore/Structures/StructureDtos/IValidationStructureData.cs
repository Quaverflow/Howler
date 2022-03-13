using FluentValidation;

namespace ExamplesCore.Structures.StructureDtos;

public interface IValidationStructureData
{
    public IValidationContext Dto { get; }
    public IValidator Validator { get; }
}