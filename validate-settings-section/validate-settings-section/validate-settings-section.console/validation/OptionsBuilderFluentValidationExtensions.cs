using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace validate_settings_section.console.validation;

public static class OptionsBuilderFluentValidationExtensions
{
    public static OptionsBuilder<TOptions> ValidateFluentValidation<TOptions>(this OptionsBuilder<TOptions> optionsBuilder) where TOptions : class
    {
        optionsBuilder.Services.AddSingleton<IValidateOptions<TOptions>>(
            provider => new FluentValidationOptions<TOptions>(optionsBuilder.Name, provider));
        return optionsBuilder;
    }
}
