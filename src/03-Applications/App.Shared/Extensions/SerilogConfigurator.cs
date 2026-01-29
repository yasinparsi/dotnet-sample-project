using Microsoft.AspNetCore.Builder;
using Serilog;
using Serilog.Exceptions;
using Serilog.Filters;
#if !DEBUG
using Serilog.Formatting.Compact;
#endif

namespace SnappFood.DotNetSampleProject.App.Shared.Extensions;

public static class SerilogConfigurator
{
    public static ConfigureHostBuilder UseDotNetSampleProjectSerilog(this ConfigureHostBuilder host)
    {
        host.UseSerilog((context, config) =>
        {
            config.Enrich.FromLogContext()
                .ReadFrom.Configuration(context.Configuration)
                .Filter.ByExcluding(Matching.WithProperty<string>("RequestMethod", v =>
                    nameof(HttpMethod.Options).Equals(v, StringComparison.OrdinalIgnoreCase)))
                .Enrich.WithMachineName()
                .Enrich.WithEnvironmentName()
                .Enrich.WithAssemblyName()
                .Enrich.WithExceptionDetails()
#if DEBUG
                .WriteTo.Async(writeTo => writeTo.Console())
#else
                .WriteTo.Async(writeTo => writeTo.Console(new CompactJsonFormatter()))
#endif
                .ReadFrom.Configuration(context.Configuration);
        });

        return host;
    }
}
