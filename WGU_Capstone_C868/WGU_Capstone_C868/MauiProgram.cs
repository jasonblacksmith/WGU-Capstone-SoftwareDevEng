using Microsoft.Extensions.Logging;
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
        builder.UseMauiApp<App>().ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
        }).UseMauiCommunityToolkit();

#if DEBUG
        builder.Logging.AddDebug();
#endif
        //builder.Services.AddSingleton<BaseViewModel>();
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<LoginPageViewModel>();
        builder.Services.AddTransient<Dashboard>();
        builder.Services.AddTransient<DashboardViewModel>();

        return builder.Build();
    }
}