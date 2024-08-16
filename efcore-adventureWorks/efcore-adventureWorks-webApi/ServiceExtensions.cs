using adventureWorks.webApi.employees;
using Microsoft.EntityFrameworkCore;

namespace adventureWorks.webApi;

public static class ServiceExtensions
{
    public static void AddEmployeeServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<EmployeeDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Default")));
        services.AddScoped<IEmployeeRepo, EmployeeRepo>();
    }
}
