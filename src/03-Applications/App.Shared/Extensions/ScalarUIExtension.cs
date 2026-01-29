using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
#if DEBUG
using Scalar.AspNetCore;
#endif

namespace SnappFood.DotNetSampleProject.App.Shared.Extensions;

public static class ScalarUIExtension
{
    public static WebApplication UseDotNetSampleProjectScalarUI(this WebApplication app)
    {
#if DEBUG
        var openapiRoute = "/openapi/template.json";
        app.MapOpenApi(openapiRoute).CacheOutput();
        app.MapScalarApiReference("/", config =>
        {
            config
                .WithOpenApiRoutePattern(openapiRoute)
                .WithTitle("DotNetSampleProject api documentation")
                .SortOperationsByMethod()
                .DisableDefaultFonts()
                .HideClientButton()
                .WithTheme(ScalarTheme.Kepler)
                .EnableDarkMode()
                .HideModels()
                .WithSearchHotKey("k");
        });
#endif

        return app;
    }
}
