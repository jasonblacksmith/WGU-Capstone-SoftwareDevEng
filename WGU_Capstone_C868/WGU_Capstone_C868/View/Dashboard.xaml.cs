namespace WGU_Capstone_C868.View;

public partial class Dashboard : ContentPage
{
	public Dashboard(User user, DashboardViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}