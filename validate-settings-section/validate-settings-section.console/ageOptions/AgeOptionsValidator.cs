using FluentValidation;

namespace validate_settings_section.console.ageOptions;

public class AgeOptionsValidator : AbstractValidator<AgeOptions>
{
    public AgeOptionsValidator()
    {
        RuleFor(x => x.Status).IsInEnum();
        RuleFor(x => x.MinWorkingAge).GreaterThanOrEqualTo(18);
        RuleFor(x => x.StartedWorkAged).GreaterThanOrEqualTo(x => x.MinWorkingAge);
        RuleFor(x => x.MinRetirementAge).GreaterThanOrEqualTo(65);
        RuleFor(x => x.RetiredAged).GreaterThanOrEqualTo(x => x.MinRetirementAge);
    }
}
