namespace StareMedic;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
		
		//Implement db validation here i think

		MainPage = new AppShell();
	}
}
