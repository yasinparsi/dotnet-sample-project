using Microsoft.Extensions.DependencyInjection;

namespace SnappFood.DotNetSampleProject.Core.ApplicationServices;

public static class ApplicationServicesBootstrapper
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediator(config =>
        {
            config.Namespace = "SnappFood.DotNetSampleProject.Mediator";
            config.ServiceLifetime = ServiceLifetime.Scoped;
        });

        return services;
    }
}
