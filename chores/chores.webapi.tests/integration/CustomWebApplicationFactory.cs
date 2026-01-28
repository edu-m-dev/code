using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace chores.webapi.tests.integration;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // Set environment BEFORE configuring services
        builder.UseEnvironment("Testing");

        builder.ConfigureAppConfiguration((context, configBuilder) =>
        {
            // Disable Azure Monitor exporter for tests
            var testOverrides = new Dictionary<string, string>
            {
                // Syntactically valid, but points nowhere meaningful
                ["AzureMonitor:ConnectionString"] = "InstrumentationKey=00000000-0000-0000-0000-000000000000;IngestionEndpoint=https://localhost/"
            };

            configBuilder.AddInMemoryCollection(testOverrides);
        });

        builder.ConfigureServices(services =>
        {
            // Remove any existing IDistributedCache (Redis) registration
            var existingCache = services
                .SingleOrDefault(d => d.ServiceType == typeof(IDistributedCache));
            if (existingCache != null)
            {
                services.Remove(existingCache);
            }

            // Replace with in-memory cache for tests
            services.AddDistributedMemoryCache();

            // Add fake authentication
            services.AddAuthentication("Test")
                .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("Test", _ => { });

            // Add allow-all authorization handler
            services.AddSingleton<IAuthorizationHandler, AllowAnonymousAuthorizationHandler>();
        });
    }
}
