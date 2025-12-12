// See https://aka.ms/new-console-template for more information

using chores.bl.ef;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

// Build configuration
var config = new ConfigurationBuilder()
    .AddJsonFile("config.json", optional: false)
    .Build();

// Setup DI
var services = new ServiceCollection();
services.AddDbContext<ChoresDbContext>(options =>
    options.UseSqlite(config.GetConnectionString("chores")));

var serviceProvider = services.BuildServiceProvider();

// Resolve and use your DbContext
using var context = serviceProvider.GetRequiredService<ChoresDbContext>();
// Example usage
var chores = context.Chores.ToList();
foreach (var chore in chores)
{
    Console.WriteLine(chore.Name);
}
