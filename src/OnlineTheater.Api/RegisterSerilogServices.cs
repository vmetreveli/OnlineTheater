using Serilog;

namespace OnlineTheater.Api;

public static class RegisterSerilogServices
{
    /// <summary>
    ///     Register the Serilog service with a custom configuration.
    /// </summary>
    private static IServiceCollection AddSerilogServices(this IServiceCollection services,
        LoggerConfiguration configuration)
    {
        Log.Logger = configuration.CreateLogger();
        AppDomain.CurrentDomain.ProcessExit += (s, e) => Log.CloseAndFlush();
        return services.AddSingleton(Log.Logger);
    }


    public static IServiceCollection AddSerilogServices(this IServiceCollection services,
        IConfiguration configuration) =>
        services.AddSerilogServices(
            new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
        );
}