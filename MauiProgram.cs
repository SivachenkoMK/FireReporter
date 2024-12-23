using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using FireReporter.Reporting;

namespace FireReporter;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
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