namespace WGU_Capstone_C868.View;

public partial class RelapseDiary : ContentPage
{
	public RelapseDiary(RelapseDiaryViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {

    }
}