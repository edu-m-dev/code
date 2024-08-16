using adventureWorks.webApi.employees;

namespace adventureWorks.webApi;

public static class EndpointExtensions
{
    public static void AddEmployeeEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/allEmployees", async (IEmployeeRepo employeeRepo, CancellationToken cancellationToken) =>
        {
            return await employeeRepo.GetAllEmployees(cancellationToken);
        })
        .WithName("GetAllEmployees")
        .WithOpenApi();
    }
}
