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


	//remember to register pages on appshell first, or you are not going nowhere
    private async void BtnPacientes_Clicked(object sender, EventArgs e)
    {
        PatientsMain.Opacity = 0;
        await PatientsMain.FadeTo(1, 200);

        await Shell.Current.GoToAsync(nameof(Pacientes));
    }

    private async void BtnRooms_Clicked(object sender, EventArgs e)
    {
        RoomsMain.Opacity = 0;
        await RoomsMain.FadeTo(1, 200);

		await Shell.Current.GoToAsync(nameof(Habitaciones));
	}

    private async void BtnRegCasoCli_Clicked(object sender, EventArgs e)
    {
        RegCCMain.Opacity = 0;
        await RegCCMain.FadeTo(1, 200);

		await Shell.Current.GoToAsync(nameof(RegisterClinicalCase));
    }

    private async void BtnSearchCC_Clicked(object sender, EventArgs e)
    {

		await Shell.Current.GoToAsync(nameof(SearchCC));
    }

    private async void BtnMedics_Clicked(object sender, EventArgs e)
    {
        MedicsMain.Opacity = 0;
        await MedicsMain.FadeTo(1, 200);

        await Shell.Current.GoToAsync(nameof(Doctores));
    }

  

}
