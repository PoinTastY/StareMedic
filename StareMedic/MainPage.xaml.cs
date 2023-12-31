﻿namespace StareMedic;

using StareMedic.Models;
using StareMedic.Views;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

	//remember to register pages on appshell first, or you are not going nowhere
    private void BtnPacientes_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(Pacientes));
    }

    private async void BtnRegister_Clicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync(nameof(RegisterPatient));
	}

	private async void BtnRooms_Clicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync(nameof(RegisterRoom));
	}

	private async void BtnMedics_Clicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync(nameof(RegisterMedic));
	}

    private async void BtnRegCasoCli_Clicked(object sender, EventArgs e)
    {
		await Shell.Current.GoToAsync(nameof(RegisterClinicalCase));
    }

    private async void BtnSearchCC_Clicked(object sender, EventArgs e)
    {
		await Shell.Current.GoToAsync(nameof(SearchCC));
    }

	private async void BtnEditPatient_Clicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync(nameof(EditPatient));
	}
}

