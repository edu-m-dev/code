using chores.bl.ef;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace chores.tests.integration;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            // Remove all ChoresDbContext registrations
            var toRemove = services
                .Where(d => d.ServiceType == typeof(DbContextOptions<ChoresDbContext>)
                         || d.ServiceType == typeof(ChoresDbContext))
                .ToList();

            foreach (var d in toRemove)
                services.Remove(d);

            // Register InMemory provider
            services.AddDbContext<ChoresDbContext>(options =>
            {
                options.UseInMemoryDatabase("ChoresTestDb");
            });
        });
    }
}
