using System.Reflection;
using EasyNetQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;
using task_practice.messages;

Serilog.Debugging.SelfLog.Enable(Console.Error); // TODO - remove
var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.RegisterEasyNetQ("host=localhost", register => register.EnableMicrosoftLogging());
    })
    .UseSerilog((hostBuilderContext, loggerConfiguration) => loggerConfiguration
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithExceptionDetails()
                .Enrich.WithProperty("Environment", environment)
                .WriteTo.Debug()
                .WriteTo.Async(wt => wt.Console())
                .WriteTo.Elasticsearch(ConfigureElasticSink(hostBuilderContext.Configuration, environment))
                .ReadFrom.Configuration(hostBuilderContext.Configuration))
    .Build();



using var bus = RabbitHutch.CreateBus("host=localhost");
await bus.PubSub.SubscribeAsync<OrderPlaced>($"orderPlacer", orderPlaced =>
{
    var serializedMessage = System.Text.Json.JsonSerializer.Serialize(orderPlaced);
    var logger = host.Services.GetRequiredService<ILogger<Program>>();
    logger.LogInformation(serializedMessage);
    Console.WriteLine($"OrderPlaced message {serializedMessage} logged");
});

await host.RunAsync();

static ElasticsearchSinkOptions ConfigureElasticSink(IConfiguration configuration, string environment)
{
    var indexName = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-{environment?.ToLower().Replace(".", "-")}";
    return new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]))
    {
        ModifyConnectionSettings = c => c.BasicAuthentication(configuration["ElasticConfiguration:Username"], configuration["ElasticConfiguration:Password"]),
        TemplateName = indexName,
        AutoRegisterTemplate = true,
        AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv8,
        IndexFormat = indexName + "-{0:yyyy.MM}", // this is a format string, it is Serilog that will interpolate it
        EmitEventFailure = EmitEventFailureHandling.WriteToSelfLog,
        RegisterTemplateFailure = RegisterTemplateRecovery.FailSink,
        // https://github.com/serilog-contrib/serilog-sinks-elasticsearch/issues/375
        TypeName = null,
        BatchAction = ElasticOpType.Create,
    };
}
