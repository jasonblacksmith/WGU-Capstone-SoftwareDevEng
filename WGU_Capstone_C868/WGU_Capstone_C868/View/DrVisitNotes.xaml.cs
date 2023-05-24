namespace WGU_Capstone_C868.View;

public partial class DrVisitNotes : ContentPage
{
	public DrVisitNotes(DrVisitNotesViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}