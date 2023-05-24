namespace WGU_Capstone_C868.View;

public partial class RelapseDiary : ContentPage
{
	public RelapseDiary(RelapseDiaryViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}