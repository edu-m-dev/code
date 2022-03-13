using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using wwwi.bl.DI;

namespace wwi.tests
{
    public class BaseTest
    {
        public BaseTest()
        {
            TestHost = CreateHostBuilder().Build();
            Task.Run(() => TestHost.RunAsync());
        }

        public IHost TestHost { get; }

        private IHostBuilder CreateHostBuilder(string[] args = null) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostingContext, services) =>
                {
                    var configuration = hostingContext.Configuration;
                    BaseDi.Configure(services, configuration);
                    ConfigureServices(services, configuration);
                });

        /// <summary>
        /// further configure services
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public virtual void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
        }
    }
}
