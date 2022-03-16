using System.Reflection;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;
using wwwi.bl.DI;

namespace wwi.web
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ConfigureLogging(builder);

            ConfigureServices(builder);

            var app = ConfigureApp(builder);

            await app.RunAsync();
        }

        private static WebApplication ConfigureApp(WebApplicationBuilder builder)
        {
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
            return app;
        }

        private static void ConfigureServices(WebApplicationBuilder builder)
        {
            var services = builder.Services;
            var configuration = builder.Configuration;

            BaseDi.Configure(services, configuration);

            services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        private static void ConfigureLogging(WebApplicationBuilder builder)
        {
            Serilog.Debugging.SelfLog.Enable(Console.Error); // TODO - remove
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            builder.Host.UseSerilog((hostBuilderContext, loggerConfiguration) => loggerConfiguration
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithExceptionDetails()
                .Enrich.WithProperty("Environment", environment)
                .WriteTo.Debug()
                .WriteTo.Async(wt => wt.Console())
                .WriteTo.Elasticsearch(ConfigureElasticSink(hostBuilderContext.Configuration, environment))
                .ReadFrom.Configuration(hostBuilderContext.Configuration));
        }

        private static ElasticsearchSinkOptions ConfigureElasticSink(IConfiguration configuration, string environment)
        {
            var indexName = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-{environment?.ToLower().Replace(".", "-")}";
            return new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]))
            {
                ModifyConnectionSettings = c => c.BasicAuthentication(configuration["ElasticConfiguration:Username"], configuration["ElasticConfiguration:Password"]),
                TemplateName = indexName,
                AutoRegisterTemplate = true,
                AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
                IndexFormat = indexName + "-{0:yyyy.MM}", // this is a format string, it is Serilog that will interpolate it
                EmitEventFailure = EmitEventFailureHandling.WriteToSelfLog,
                RegisterTemplateFailure = RegisterTemplateRecovery.FailSink,
                // https://github.com/serilog-contrib/serilog-sinks-elasticsearch/issues/375
                TypeName = null,
                BatchAction = ElasticOpType.Create,
            };
        }
    }
}
