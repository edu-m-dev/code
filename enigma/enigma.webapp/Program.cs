var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

builder.Services.AddOpenApiDocument(config =>
{
    config.Title = "enigma api";
    config.Version = "v1";
});


var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
app.UseDeveloperExceptionPage();
app.UseOpenApi();     // Serves /swagger/v1/swagger.json
app.UseSwaggerUi();  // Serves Swagger UI at /swagger
//}

app.MapControllers();

app.Run();

public partial class Program { } // make it accessible to WebApplicationFactory<Program>
