namespace WGU_Capstone_C868.View;

public partial class MetricsData : ContentPage
{
	public MetricsData(MetricsDataViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}