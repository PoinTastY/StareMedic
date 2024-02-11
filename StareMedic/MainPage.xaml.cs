namespace StareMedic;

using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using StareMedic.Models;
using StareMedic.Views;
using StareMedic.Views.Viewers;
using Microsoft.Maui.Controls;



public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();

       
    }

    private void OnHamburguerClicked(object sender, EventArgs e)
    {
        sidePanel.IsVisible = !sidePanel.IsVisible;
    }

	//remember to register pages on appshell first, or you are not going nowhere
    private void BtnPacientes_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(Pacientes));
    }

	private async void BtnRooms_Clicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync(nameof(Habitaciones));
	}

    private async void BtnRegCasoCli_Clicked(object sender, EventArgs e)
    {
		await Shell.Current.GoToAsync(nameof(RegisterClinicalCase));
    }

    private async void BtnSearchCC_Clicked(object sender, EventArgs e)
    {
		await Shell.Current.GoToAsync(nameof(SearchCC));
    }

    private async void BtnMedics_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(Doctores));
    }
}

