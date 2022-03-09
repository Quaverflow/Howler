using FluentValidation;
using HowlerExamples.Models;

namespace HowlerExamples.Structures;

public class Validator : AbstractValidator<Dto>
{
    public Validator()
    {
        RuleFor(x => x.Age)
            .GreaterThan(18).WithMessage("You must be over 18 to see this!");
        RuleFor(x => x.Name)
            .NotEqual("Chris").WithMessage("Because your boss should never know what you're really up to.");
        RuleFor(x => x.Surname)
            .Must(x => x.Contains("p", StringComparison.CurrentCultureIgnoreCase)).WithMessage("I like 'p's");
    }
}