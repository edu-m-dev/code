using adventureWorks.webApi.employees;

namespace adventureWorks.webApi;

public static class EndpointExtensions
{
    public static void AddEmployeeEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/allEmployees", async (IEmployeeRepo employeeRepo, ILogger<Employee> logger, CancellationToken cancellationToken) => // TODO - ILogger type?
        {
            var employees = await employeeRepo.GetAllEmployees(cancellationToken);
            logger.LogInformation("Returned {EmployeeCount} employees", employees.Count());
            return employees;
        })
        .WithName("GetAllEmployees")
        .WithOpenApi();
    }
}
