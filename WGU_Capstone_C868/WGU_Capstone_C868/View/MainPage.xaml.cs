﻿namespace WGU_Capstone_C868;

public partial class MainPage : ContentPage
{
	public MainPage(LoginPageViewModel viewModel)
	{
		InitializeComponent();
        Task task0 = StartDb();
		Task task1 = LoadMoqData();
		BindingContext = viewModel;
	}

	private async Task StartDb()
	{
        await SqLiteDataService.Init();
    }

	private async Task LoadMoqData()
	{
		await MoqDataLoader.Init();
	}
}
