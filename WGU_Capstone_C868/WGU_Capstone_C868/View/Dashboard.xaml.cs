using WGU_Capstone_C868.Services.Calls;

namespace WGU_Capstone_C868.View;

public partial class Dashboard : ContentPage
{
    public Dashboard(DashboardViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}