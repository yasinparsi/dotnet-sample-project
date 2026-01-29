using Serilog;
using System.Text;
#if !DEBUG
using Serilog.Formatting.Compact;
#endif

namespace SnappFood.DotNetSampleProject.App.Shared.Extensions;

public static class AppLevelLogger
{
    public static void LogStarting(string applicationName)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Log.Logger = new LoggerConfiguration()
#if DEBUG
                    .WriteTo.Console()
#else
            .WriteTo.Console(new CompactJsonFormatter())
#endif
            .CreateBootstrapLogger();

        Log.Information("{ApplicationName} application start-up completed, {DateTime}, {SecurityLog}",
            applicationName, DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss"), 1);
    }

    public static void LogStopException(string applicationName, Exception ex)
    {
        Log.Fatal(ex, "Unhandled exception occured in {ApplicationName} application, {DateTime}, {SecurityLog}",
            applicationName, DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss"), 1);
    }

    public static void LogFinalizing(string applicationName)
    {
        Log.Information("{ApplicationName} application shut-down completed, {DateTime}, {SecurityLog}",
            applicationName, DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss"), 1);
        Log.CloseAndFlush();
    }
}
