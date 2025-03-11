using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using FireReporter.Reporting;
using Microsoft.Extensions.Logging.Debug;

namespace FireReporter;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.Services.AddHttpClient(Constants.HttpClientName, client =>
        {
            client.BaseAddress = new Uri("https://api.firetracker.sivach.me/");
        });
        
        // Configure logging
        builder.Logging.ClearProviders(); // Clear default providers if needed
        builder.Logging.AddProvider(new DebugLoggerProvider()); // Add console provider or any other (e.g., Debug, File)

        // Set logging level to Information and above (you can set to Warning/Error to reduce noise)
        builder.Logging.SetMinimumLevel(LogLevel.Information);
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddTransient<Views.FireReportingPage>();
        builder.Services.AddSingleton<UserState>();
#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}