namespace WGU_Capstone_C868;

public partial class MainPage : ContentPage
{
	public MainPage(LoginPageViewModel viewModel)
	{
		InitializeComponent();
		async void Init()
		{
			//TODO: Figure this out!
			SqLiteDataService db;
		}
		BindingContext = viewModel;
	}
}
