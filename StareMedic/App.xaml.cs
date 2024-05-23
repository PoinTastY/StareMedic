﻿namespace StareMedic;

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

        

        window.Title = "StareMedic";
        

        return window;
    }



}
