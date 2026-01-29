using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;

namespace SnappFood.DotNetSampleProject.App.Shared.Extensions;

public static class OpenTelemetryExtensions
{
    public static IServiceCollection AddDotNetSampleProjectOpenTelemetry(this IServiceCollection services)
    {
        services.AddOpenTelemetry()
            .ConfigureResource(resource =>
            {
                resource.AddService(
                    serviceName: "DotNetSampleProject",
                    serviceVersion: "1.0.0",
                    serviceInstanceId: Environment.MachineName);
            })
            .WithMetrics(metrics =>
            {
                metrics.AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddRuntimeInstrumentation()
                    .AddProcessInstrumentation()
                    .AddPrometheusExporter();
            });

        return services;
    }

    public static WebApplication UseDotNetSampleProjectOpenTelemetry(this WebApplication app)
    {
        app.MapPrometheusScrapingEndpoint("/metrics");
        return app;
    }
}
