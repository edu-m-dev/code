using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using wwi.bl.EF;

namespace wwwi.bl.DI
{
    public class BaseDi
    {
        public static IServiceCollection Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextFactory<WwiDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("wwi")));

            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddLazyCache();

            return services;
        }
    }
}
