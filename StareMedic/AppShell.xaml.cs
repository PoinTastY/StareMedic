 
using CommunityToolkit.Maui.Views;
using StareMedic.Views;
using StareMedic.Views.Viewers;

namespace StareMedic;

public partial class AppShell : Shell
{
    private SemaphoreSlim semaforo = new(1, 1);
    
	public AppShell()
	{
		InitializeComponent();
        //App
        Shell.SetBackButtonBehavior(this, new BackButtonBehavior { IsVisible = false } );
        //Routing
        Routing.RegisterRoute(nameof(Pacientes), typeof(Pacientes));
        Routing.RegisterRoute(nameof(Doctores), typeof(Doctores));
		Routing.RegisterRoute(nameof(Habitaciones), typeof(Habitaciones));
        Routing.RegisterRoute(nameof(RegisterClinicalCase), typeof(RegisterClinicalCase));
		Routing.RegisterRoute(nameof(PickPatientView), typeof(PickPatientView));
		Routing.RegisterRoute(nameof(SearchCC), typeof(SearchCC));
		Routing.RegisterRoute(nameof(ViewClinicalCase), typeof(ViewClinicalCase));
		Routing.RegisterRoute(nameof(MedicControll), typeof(MedicControll));
		Routing.RegisterRoute(nameof(PatientControll), typeof(PatientControll));
		Routing.RegisterRoute(nameof(RoomControll), typeof(RoomControll));
    }

    private async void BtnPacientes_Clicked(object sender, EventArgs e)
    {
        if (!await semaforo.WaitAsync(0))
            return;

        var button = sender as Button;
        if (button != null)
        {
            button.IsEnabled = false; // Deshabilitar el botón para evitar clics múltiples
        }

        var popup = new SpinnerPopup();
        this.ShowPopup(popup);
        try
        {
            PatientsFly.Opacity = 0;
            await PatientsFly.FadeTo(1, 200);
            await Shell.Current.GoToAsync(nameof(Pacientes));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Ocurrió un error al navegar a la página de pacientes: {ex}.", "OK");
        }
        finally
        {
            popup.Close();

            if (button != null)
            {
                button.IsEnabled = true; // Rehabilitar el botón al final
            }
            semaforo.Release(); // Asegurarse de liberar el semáforo
        }
    }


    private async void BtnRooms_Clicked(object sender, EventArgs e)
    {
        if (!await semaforo.WaitAsync(0))
            return;
        
        var button = sender as Button;
        if (button != null)
        {
            button.IsEnabled = false; // Deshabilitar el botón para evitar clics múltiples
        }
        var popup = new SpinnerPopup();
        this.ShowPopup(popup);
        try
        {
            await Shell.Current.GoToAsync(nameof(Habitaciones));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Ocurrió un error al navegar a la página de Habitaciones {ex}.", "OK");
        }
        finally
        {
            popup.Close();
            if (button != null)
            {
                button.IsEnabled = true; // Rehabilitar el botón al final
            }
            semaforo.Release(); // Asegurarse de liberar el semáforo
        }

    }

    private async void BtnRegCasoCli_Clicked(object sender, EventArgs e)
    {
        if (!await semaforo.WaitAsync(0))
            return;

        var button = sender as Button;
        if (button != null)
        {
            button.IsEnabled = false;
        }

        var popup = new SpinnerPopup();
        this.ShowPopup(popup);
        try
        {
            RoomsFly.Opacity = 0;
            await RoomsFly.FadeTo(1, 200);

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
                button.IsEnabled = true;
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
            button.IsEnabled = false;
        }

        var popup = new SpinnerPopup();
        this.ShowPopup(popup);
        try
        {
            CCFly.Opacity = 0;
            await CCFly.FadeTo(1, 200);

            await Shell.Current.GoToAsync(nameof(SearchCC));
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
                button.IsEnabled = true;
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
            button.IsEnabled = false;
        }

        var popup = new SpinnerPopup();
        this.ShowPopup(popup);
        try
        {
            MedicsFly.Opacity = 0;
            await MedicsFly.FadeTo(1, 200);

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
                button.IsEnabled = true;
            }
            semaforo.Release();
        }
    }

    private async void BtnAllBack_Clicked(object sender, EventArgs e)
    {
        if (!await semaforo.WaitAsync(0))
            return;

        var button = sender as Button;
        if (button != null)
        {
            button.IsEnabled = false;
        }

        var popup = new SpinnerPopup();
        this.ShowPopup(popup);
        try
        {
            MainMenu.Opacity = 0;

            await MainMenu.FadeTo(1, 200);
            await Shell.Current.GoToAsync("//MainPage");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Ocurrió un error al navegar a la página Principal: {ex}.", "OK");
        }
        finally
        {
            popup.Close();
            if (button != null)
            {
                button.IsEnabled = true;
            }
            semaforo.Release();
        }
    }
}