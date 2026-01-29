using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SnappFood.DotNetSampleProject.Core.DomainModels.General;
using SnappFood.DotNetSampleProject.Core.DomainServices.Abstractions;
using SnappFood.DotNetSampleProject.Infrastructure.DomainPractice.Configs;
using SnappFood.DotNetSampleProject.Infrastructure.DomainPractice.DbContexts;
using SnappFood.DotNetSampleProject.Infrastructure.DomainPractice.DomainServices;

namespace SnappFood.DotNetSampleProject.Infrastructure.DomainPractice;

public static class DomainPracticeBootstrapper
{
    public static IServiceCollection AddDomainPractice(this IServiceCollection services, Action<DomainPracticeConfigs> config)
    {
        DomainPracticeConfigs newConfig = new();
        config.Invoke(newConfig);

        services.AddDbContextPool<AppDbContext>(opt =>
        {
            opt.UseInMemoryDatabase(newConfig.PrimaryConnectionString);
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IBikerService, BikerService>();
        services.AddScoped<IQuestService, QuestService>();

        return services;
    }

}
