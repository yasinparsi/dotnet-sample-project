using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.DependencyInjection;

namespace SnappFood.DotNetSampleProject.App.Shared.Extensions;

public static class HealthCheckExtensions
{
    public static WebApplicationBuilder AddDotNetSampleProjectHealthChecks(this WebApplicationBuilder builder)
    {
        builder.Services.AddHealthChecks();

        //Add dependencies

        //.AddMySql(
        //    connectionString: builder.Configuration.GetConnectionString("DefaultConnection")!,
        //    healthQuery: "SELECT 1;"
        //)
        //.AddRedis(builder.Configuration.GetConnectionString("Redis")!);

        return builder;
    }

    public static WebApplication UseDotNetSampleProjectHealthChecks(this WebApplication app)
    {
        app.MapHealthChecks("/healthz", new HealthCheckOptions()
        {
            Predicate = _ => true,
        });
        return app;
    }
}