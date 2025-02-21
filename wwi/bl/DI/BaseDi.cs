using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using wwi.bl.EF;

namespace wwwi.bl.DI
{
    public class BaseDi
    {
        public static IServiceCollection Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextFactory<WwiDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("wwi"))
                    .LogTo(Console.WriteLine, LogLevel.Information));

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddLazyCache();

            return services;
        }
    }
}
