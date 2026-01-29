using SnappFood.DotNetSampleProject.App.Shared.Extensions;
using SnappFood.DotNetSampleProject.Core.ApplicationServices;
using SnappFood.DotNetSampleProject.Infrastructure.DomainPractice;
using System.Text.Json;

try
{
    AppLevelLogger.LogStarting("DotNetSampleProjectWeb");

    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddControllersWithViews()
        .AddJsonOptions(o =>
        {
            o.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            o.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
        });

    builder.Services.AddApplicationServices();
    builder.Services.AddDomainPractice(config =>
    {
        config.PrimaryConnectionString = "DotNetSampleProjectConnectionString";
    });

    builder.AddDotNetSampleProjectHealthChecks();
    builder.Services.AddDotNetSampleProjectOpenTelemetry();

    builder.Host.UseDotNetSampleProjectSerilog();
    var app = builder.Build();

    app.UseMiddleware<ExceptionMiddleware>();

    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();

    app.UseDotNetSampleProjectHealthChecks();
    app.UseDotNetSampleProjectOpenTelemetry();

    app.UseRouting();

    app.UseAuthorization();

    app.MapStaticAssets();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
        .WithStaticAssets();


    app.Run();
}
catch (Exception ex) when (ex.GetType().Name is not "StopTheHostException")
{
    AppLevelLogger.LogStopException("DotNetSampleProjectWeb", ex);
}
finally
{
    AppLevelLogger.LogFinalizing("DotNetSampleProjectWeb");
}
