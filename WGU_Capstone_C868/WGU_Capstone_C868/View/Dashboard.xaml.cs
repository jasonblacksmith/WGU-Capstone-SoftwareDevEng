using Microsoft.Maui.Controls;
using WGU_Capstone_C868.Services.Calls;
using WGU_Capstone_C868.ViewModel;

namespace WGU_Capstone_C868.View;

public partial class Dashboard : ContentPage
{
    public Dashboard(DashboardViewModel viewModel)
	{
        InitializeComponent();
        BindingContext = viewModel;
        Task task = LoadThisData();
    }

    private async Task LoadThisData()
    {
        await LoadData.Init();
    }
}