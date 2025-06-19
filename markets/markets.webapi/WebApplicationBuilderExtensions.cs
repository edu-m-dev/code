using Scalar.AspNetCore;

namespace markets.webapi
{
    public static class WebApplicationBuilderExtensions
    {
        public static void AddOpenApiServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer(); // Required for OpenAPI metadata
            builder.Services.AddOpenApi();
        }
        
        public static void UseOpenApi(this WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }
        }
        public static void AddMarketsServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ITickerService, TickerService>();
        }
    }
}
