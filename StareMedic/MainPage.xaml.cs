using StareMedic.Views;
using StareMedic.Views.Viewers;
using CommunityToolkit.Maui.Views;

namespace StareMedic;

public partial class MainPage : ContentPage
{
    private bool navegando = false;
    private SemaphoreSlim semaforo = new(1, 1);

    public MainPage()
	{
		InitializeComponent();
    }


	//remember to register pages on appshell first, or you are not going nowhere
    private async void BtnPacientes_Clicked(object sender, EventArgs e)
    {
        if (!await semaforo.WaitAsync(0))
            return;//si no se puede obtener el semaforo, significa q ya hay chamba y retorna

        var button = sender as Button;
        if (button != null)
        {
            button.IsEnabled = false; // Deshabilitar el botón inmediatamente
        }

        var popup = new SpinnerPopup();
        this.ShowPopup(popup);
        try
        {
            PatientsMain.Opacity = 0;
            await PatientsMain.FadeTo(1, 200);
            await Shell.Current.GoToAsync(nameof(Pacientes));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Ocurrió un error al navegar a la página de Pacientes: {ex}.", "OK");
        }
        finally
        {
            popup.Close();
            if (button != null)
            {
                button.IsEnabled = true; // Rehabilitar el botón al final
            }
            semaforo.Release();
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
        if(!await semaforo.WaitAsync(0))
                return;

        var button = sender as Button;
        if (button != null)
        {
            button.IsEnabled = false; // Deshabilitar el botón inmediatamente
        }
        var popup = new SpinnerPopup();
        this.ShowPopup(popup);
        try
        {
            RegCCMain.Opacity = 0;
            await RegCCMain.FadeTo(1, 200);
            await Shell.Current.GoToAsync(nameof(RegisterClinicalCase));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Ocurrió un error al navegar a la página de Admisiones Clinicas: {ex}.", "OK");
        }
        finally
        {
            popup.Close();
            if (button != null)
            {
                button.IsEnabled = true; // Rehabilitar el botón al final
            }
            semaforo.Release();
        }
    }

    private async void BtnSearchCC_Clicked(object sender, EventArgs e)
    {
        if (!await semaforo.WaitAsync(0))
            return;
        var button = sender as Button;
        if (button != null)
        {
            button.IsEnabled = false; // Deshabilitar el botón inmediatamente
        }
        var popup = new SpinnerPopup();
        this.ShowPopup(popup);

        try
        {
            Search.Opacity = 0;

            await Search.FadeTo(1, 200);

            // Realizar la navegación
            await Shell.Current.GoToAsync(nameof(SearchCC));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Ocurrió un error al navegar a la página de Admisiones Clinicas: {ex}.", "OK");
        }
        finally
        {
            // Cerrar el popup después de la navegación
            popup.Close();
            if (button != null)
            {
                button.IsEnabled = true; // Rehabilitar el botón al final
            }
            semaforo.Release();
        }
    }

    private async void BtnMedics_Clicked(object sender, EventArgs e)
    {
        if (!await semaforo.WaitAsync(0))
            return;
        var button = sender as Button;
        if (button != null)
        {
            button.IsEnabled = false; // Deshabilitar el botón inmediatamente
        }
        var popup = new SpinnerPopup();
        this.ShowPopup(popup);
        try
        {
            MedicsMain.Opacity = 0;
            await MedicsMain.FadeTo(1, 200);
            await Shell.Current.GoToAsync(nameof(Doctores));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Ocurrió un error al navegar a la página de Medicos: {ex}.", "OK");
        }
        finally
        {
            popup.Close();
            if (button != null)
            {
                button.IsEnabled = true; // Rehabilitar el botón al final
            }
            semaforo.Release();
        }
    }
}
