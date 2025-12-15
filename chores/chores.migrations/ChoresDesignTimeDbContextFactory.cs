using chores.bl.ef;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

// Design-time factory used by EF Core tools to create the DbContext when running migrations
public class ChoresDesignTimeDbContextFactory : IDesignTimeDbContextFactory<ChoresDbContext>
{
    public ChoresDbContext CreateDbContext(string[] args)
    {
        // Build configuration from appsettings.json + environment variables
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .AddEnvironmentVariables()
            .Build();

        // Get connection string named "chores"
        var conn = config.GetConnectionString("chores");

        var builder = new DbContextOptionsBuilder<ChoresDbContext>();
        builder.UseSqlServer(conn, sqlOptions => sqlOptions.MigrationsAssembly("chores.migrations"));

        return new ChoresDbContext(builder.Options);
    }
}
