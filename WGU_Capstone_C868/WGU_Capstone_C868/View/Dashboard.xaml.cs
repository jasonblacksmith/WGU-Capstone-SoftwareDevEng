using WGU_Capstone_C868.Services.Calls;
using WGU_Capstone_C868.ViewModel;

namespace WGU_Capstone_C868.View;

public partial class Dashboard : ContentPage
{
    public Dashboard(DashboardViewModel viewModel)
	{
        BindingContext = viewModel;
        _ = viewModel.Init();
        InitializeComponent();
    }
}