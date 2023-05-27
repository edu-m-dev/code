using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using validate_settings_section.console.ageOptions;
using validate_settings_section.console.validation;

Host.CreateDefaultBuilder()
    .ConfigureServices(services =>
    {
        services.AddScoped<IValidator<AgeOptions>, AgeOptionsValidator>();

        services.AddOptions<AgeOptions>()
            .BindConfiguration(AgeOptions.SectionName)
            .ValidateFluentValidation()
            .ValidateOnStart();

        services.AddHostedService<AgeOptionsService>();
    })
    .Build().Run();
