namespace validate_settings_section.console.ageOptions;

public record AgeOptions
{
    public const string SectionName = "AgeOptions";
    public required WorkerStatus Status { get; init; }
    public required int MinWorkingAge { get; init; }
    public required int StartedWorkAged { get; init; }
    public required int MinRetirementAge { get; init; }
    public required int RetiredAged { get; init; }
}
