using SnappFood.DotNetSampleProject.App.Shared.Extensions;
using SnappFood.DotNetSampleProject.Core.ApplicationServices;
using SnappFood.DotNetSampleProject.Infrastructure.DomainPractice;
using System.Text.Json;

try
{
    AppLevelLogger.LogStarting("DotNetSampleProjectApi");

    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddControllers()
        .AddJsonOptions(o =>
        {
            o.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            o.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
        });

    builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

    builder.Services.AddApplicationServices();
    builder.Services.AddDomainPractice(config =>
    {
        config.PrimaryConnectionString = "DotNetSampleProjectConnectionString";
    });

    builder.Services.AddOpenApi();

    builder.AddDotNetSampleProjectHealthChecks();
    builder.Services.AddDotNetSampleProjectOpenTelemetry();

    builder.Host.UseDotNetSampleProjectSerilog();
    var app = builder.Build();

    app.UseMiddleware<ExceptionMiddleware>();

    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
    }

    app.UseHttpsRedirection();

    app.UseDotNetSampleProjectHealthChecks();
    app.UseDotNetSampleProjectOpenTelemetry();

    app.UseAuthorization();

#if DEBUG
    app.UseDotNetSampleProjectScalarUI();
#endif

    app.MapControllers();

    app.Run();
}
catch (Exception ex) when (ex.GetType().Name is not "StopTheHostException")
{
    AppLevelLogger.LogStopException("DotNetSampleProjectApi", ex);
}
finally
{
    AppLevelLogger.LogFinalizing("DotNetSampleProjectApi");
}
