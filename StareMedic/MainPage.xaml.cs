namespace StareMedic;

using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using StareMedic.Models;
using StareMedic.Views;
using StareMedic.Views.Viewers;
using Microsoft.Maui.Controls;
using CommunityToolkit.Maui.Views;

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

        var popup = new SpinnerPopup();
        this.ShowPopup(popup);
        try
        {
            await Shell.Current.GoToAsync(nameof(Pacientes));
        }
        finally
        {
            popup.Close();
        }
    }

    /*private async void BtnRooms_Clicked(object sender, EventArgs e)
    {
        RoomsMain.Opacity = 0;
        await RoomsMain.FadeTo(1, 200);

		await Shell.Current.GoToAsync(nameof(Habitaciones));
	}         */

    private async void BtnRegCasoCli_Clicked(object sender, EventArgs e)
    {
        RegCCMain.Opacity = 0;
        await RegCCMain.FadeTo(1, 200);

        var popup = new SpinnerPopup();
        this.ShowPopup(popup);
        try
        {
            await Shell.Current.GoToAsync(nameof(RegisterClinicalCase));
        }
        finally
        {
            popup.Close();
        }

    }

    private async void BtnSearchCC_Clicked(object sender, EventArgs e)
    {
        Search.Opacity = 0;

        var popup = new SpinnerPopup();
        this.ShowPopup(popup);

        await Search.FadeTo(1, 200); 
        try
        {
            // Realizar la navegación
            await Shell.Current.GoToAsync(nameof(SearchCC));
        }
        finally
        {
            // Cerrar el popup después de la navegación
            popup.Close();
        }
    }

    private async void BtnMedics_Clicked(object sender, EventArgs e)
    {
        MedicsMain.Opacity = 0;
        await MedicsMain.FadeTo(1, 200);
        var popup = new SpinnerPopup();
        this.ShowPopup(popup);
        try
        {
            await Shell.Current.GoToAsync(nameof(Doctores));
        }
        finally
        {
            popup.Close();
        }
    }
}
