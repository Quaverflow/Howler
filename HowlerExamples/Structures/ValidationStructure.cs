using FluentValidation;
using HowlerExamples.Structures.StructureDtos;

namespace HowlerExamples.Structures;

public class ValidationStructure : IValidationStructure
{
    public async Task Validate(IValidationStructureData validationData)
    {
        var result = await validationData.Validator.ValidateAsync(validationData.Dto);
        if (!result.IsValid)
        {
            throw new ValidationException(result.Errors);
        }
    }
}