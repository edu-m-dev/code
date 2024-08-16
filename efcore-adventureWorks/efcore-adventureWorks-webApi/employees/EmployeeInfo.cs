namespace adventureWorks.webApi.employees;

public record EmployeeInfo
{
    // employee
    public required int BusinessEntityId { get; init; }

    public required string NationalIdnumber { get; init; }

    public required string LoginId { get; init; }

    public required short? OrganizationLevel { get; init; }

    public required string JobTitle { get; init; }

    public required DateOnly BirthDate { get; init; }

    public required string MaritalStatus { get; init; }

    public required string Gender { get; init; }

    public required DateOnly HireDate { get; init; }

    public required bool SalariedFlag { get; init; }

    public required short VacationHours { get; init; }

    public required short SickLeaveHours { get; init; }

    public required bool CurrentFlag { get; init; }

    public required Guid EmployeeRowguid { get; init; }

    public required DateTime EmployeeModifiedDate { get; init; }

    // person
    public required string PersonType { get; init; }

    public required bool NameStyle { get; init; }

    public required string? Title { get; init; }

    public required string FirstName { get; init; }

    public required string? MiddleName { get; init; }

    public required string LastName { get; init; }

    public required string? Suffix { get; init; }

    public required int EmailPromotion { get; init; }

    public required string? AdditionalContactInfo { get; init; }

    public required string? Demographics { get; init; }

    public required Guid PersonRowguid { get; init; }

    public required DateTime PersonModifiedDate { get; init; }
}
