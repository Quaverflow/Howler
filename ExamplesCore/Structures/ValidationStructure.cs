using ExamplesCore.Structures.StructureDtos;
using FluentValidation;

namespace ExamplesCore.Structures;

public class ValidationStructure : IValidationStructure
{
    public async Task ValidateAsync(IValidationStructureData validationData)
    {
        var result = await validationData.Validator.ValidateAsync(validationData.Dto);
        if (!result.IsValid)
        {
            throw new ValidationException(result.Errors);
        }
    }
}