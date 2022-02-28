using MediatR;
using Microsoft.EntityFrameworkCore;
using wwi.bl.EF;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = builder.Configuration;
builder.Services.AddDbContextFactory<WwiDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("wwi")));

builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
