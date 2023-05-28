namespace WGU_Capstone_C868;

public partial class App : Application
{
	public App()
	{
		Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjIxNDg1MkAzMjMxMmUzMDJlMzBGN2hQZStVWk81TWJ2T29SeGlVY09WcGtlZitjdHFSNXBOVXdORGRnMXFvPQ==");
		InitializeComponent();

		MainPage = new AppShell();
	}
}
