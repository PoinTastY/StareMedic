

namespace StareMedic;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
		
		//Implement db validation here i think

		MainPage = new AppShell();

    }

    protected override Window CreateWindow(IActivationState activationState)
    {
        var window = base.CreateWindow(activationState);

        const int maxWidth = 1800;
        const int maxHeight = 1100;

        const int minWidth = 1400;
        const int minHeight = 800;



        window.X = 100;
        window.Y = 10;

        window.Width = maxWidth;
        window.Height = maxHeight;

        window.MinimumWidth = minWidth;
        window.MinimumHeight = minHeight;

        window.MaximumWidth = maxWidth;
        window.MaximumHeight = maxHeight;


        window.Title = "StareMedic";
        

        return window;
    }



}
