using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Core.Hosting;
using WGU_Capstone_C868.Services;
using WGU_Capstone_C868;
using WGU_Capstone_C868.ViewModel;
using WGU_Capstone_C868.View;
using CommunityToolkit.Maui;

namespace WGU_Capstone_C868;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureSyncfusionCore()
            .ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
        })
            .UseMauiCommunityToolkit();

#if DEBUG
        builder.Logging.AddDebug();
#endif
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<LoginPageViewModel>();
        builder.Services.AddTransient<Dashboard>();
        builder.Services.AddTransient<DashboardViewModel>();
        builder.Services.AddTransient<ImageOrLabPage>();
        builder.Services.AddTransient<ImgOrLabViewModel>();
        builder.Services.AddTransient<RelapseDiary>();
        builder.Services.AddTransient<RelapseDiaryViewModel>();
        builder.Services.AddTransient<MetricsData>();
        builder.Services.AddTransient<MetricsDataViewModel>();

        return builder.Build();
    }
}