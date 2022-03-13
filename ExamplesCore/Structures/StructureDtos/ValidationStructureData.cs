using FluentValidation;

namespace ExamplesCore.Structures.StructureDtos;

public class ValidationStructureData<T, TValidator> : IValidationStructureData
    where T: class where TValidator : IValidator<T>, new()
{
    public ValidationStructureData(T dto)
    {
        Dto = new ValidationContext<T>(dto);
        Validator = new TValidator();
    }

    public IValidationContext Dto { get; }
    public IValidator Validator { get; }
}