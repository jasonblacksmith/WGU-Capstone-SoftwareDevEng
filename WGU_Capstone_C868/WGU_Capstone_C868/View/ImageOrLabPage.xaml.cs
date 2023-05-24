using Microsoft.Maui.Controls;
using WGU_Capstone_C868.Services.Calls;
using WGU_Capstone_C868.ViewModel;

namespace WGU_Capstone_C868.View;

public partial class ImageOrLabPage : ContentPage
{
    public ImageOrLabPage(ImgOrLabViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}