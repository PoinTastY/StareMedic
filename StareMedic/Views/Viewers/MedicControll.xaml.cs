namespace StareMedic.Views.Viewers;
using StareMedic.Models;
using StareMedic.Models.Entities;

public partial class MedicControll : ContentPage
{
    private Medic doctor;
    public MedicControll(Medic doctor)
    {
        InitializeComponent();
        if (doctor != null)
        {
            this.doctor = doctor;
            EntryName.Text = doctor.Nombre;
            EntryTelefono.Text = doctor.Telefono;
            EntryDomicilio.Text = doctor.Domicilio;
            EntryCiudad.Text = doctor.Ciudad;
            EntryEstado.Text = doctor.Estado;
            EnableDisable(false);
        }
        else
        {
            this.doctor = new(MainRepo.GetCurrentMedicIndex());
        }
    }

    private void EntryName_TextChanged(object sender, TextChangedEventArgs e)
    {
        doctor.Nombre = EntryName.Text;
    }

    private void EntryTelefono_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(e.NewTextValue))
        {
            if (e.NewTextValue == null || e.NewTextValue == " ")
                EntryTelefono.Text = e.OldTextValue;
            if (!ValidationHelper.IsNumeric(e.NewTextValue))
                EntryTelefono.Text = e.OldTextValue;
        }
        else
        {
            doctor.Telefono = EntryTelefono.Text;
        }
    }

    private void EntryDomicilio_TextChanged(object sender, TextChangedEventArgs e)
    {
        doctor.Domicilio = EntryDomicilio.Text;
    }

    private void EntryCiudad_TextChanged(object sender, TextChangedEventArgs e)
    {
        doctor.Ciudad = EntryCiudad.Text;
    }

    private void EntryEstado_TextChanged(object sender, TextChangedEventArgs e)
    {
        doctor.Estado = EntryEstado.Text;
    }

    private async void BtnGuardar_Clicked(object sender, EventArgs e)
    {
        BtnGuardar.Opacity = 0;

        await BtnGuardar.FadeTo(1, 300);

        if (doctor)
        {
            bool confirm = await DisplayAlert("Confirmar", $"Se registrara al Medico:\n{doctor.Nombre}", "Cancelar", "Confirmar");
            if (!confirm)
            {
                MainRepo.AddMedic(doctor);
                //create a method to confirm directly in the database, return a bool and make an if 4 below
                await DisplayAlert("Confirmado", $"Se ha registrado al Medico:\n{doctor.Nombre}", "Ok");
                await Shell.Current.GoToAsync("..");
            }
        }
        else
        {
            await DisplayAlert("Error", $"Datos Faltantes: {ValidarFaltantes(doctor)}", "Ok");
        }
    }

    private async void BtnCancel_Clicked(object sender, EventArgs e)
    {
        BtnCancel.Opacity = 0;

        await BtnCancel.FadeTo(1, 300);
        bool confirm = await DisplayAlert("Cancelar", "Desea cancelar el registro?", "No", "Si");
        if (!confirm)
        {
            await DisplayAlert("Cancelado", "No se ha realizado ningun cambio", "Ok");
            await Navigation.PopAsync();
        }
    }

    private async void BtnCancel2_Clicked(object sender, EventArgs e)
    {
        BtnCancel.Opacity = 0;

        await BtnCancel.FadeTo(1, 300);

        await Shell.Current.GoToAsync("..");
    }


    private async void BtnDelete_Clicked(object sender, EventArgs e)
    {
        BtnDelete.Opacity = 0;

        await BtnDelete.FadeTo(1, 300);
        bool confirm = await DisplayAlert("Eliminar", $"Desea eliminar al Doctor: {doctor.Nombre}?", "No", "Si");
        if (!confirm)
        {
            var done = MainRepo.DeleteMedic(doctor.Id);
            if (done)
            {
                await DisplayAlert("Exito", $"Medico eliminado: {doctor.Nombre}", "OK");
                await Shell.Current.GoToAsync("..");
            }
            else
                await DisplayAlert("Error", $"El medico tiene admisiones registradas", "OK");
        }
        else
        {
            await DisplayAlert("Cancelado", "Operacion cancelada, no se ha borrado nada", "OK");
            return;
        }
    }

    private void EnableDisable(bool x)
    {
        BtnEdit.IsVisible = !x;
        BtnEdit.IsEnabled = !x;
        BtnDelete.IsVisible = !x;
        BtnDelete.IsEnabled = !x;
        EntryName.IsEnabled = x;
        EntryTelefono.IsEnabled = x;
        EntryDomicilio.IsEnabled = x;
        EntryCiudad.IsEnabled = x;
        EntryEstado.IsEnabled = x;
        BtnCancel.IsVisible = x;
        BtnCancel.IsEnabled = x;
        BtnCancel2.IsVisible = !x;
        BtnCancel2.IsEnabled = !x;
        BtnGuardar.IsVisible = x;
        BtnGuardar.IsEnabled = x;
    }

    private async void BtnEdit_Clicked(object sender, EventArgs e)
    {
        BtnEdit.Opacity = 0;

        await BtnEdit.FadeTo(1, 300);

        EnableDisable(true);
    }
    private string ValidarFaltantes(Medic fallo)
    {
        string faltantes = "";

        if (string.IsNullOrEmpty(fallo.Nombre))
        {
            faltantes += " Nombre ";
        }
        if (string.IsNullOrEmpty(fallo.Domicilio))
        {
            faltantes += " Domicilio ";
        }

        if (string.IsNullOrEmpty(fallo.Ciudad))
        {
            faltantes += " Ciudad ";
        }

        if (string.IsNullOrEmpty(fallo.Estado))

            faltantes += " Estado ";

        return faltantes;

    }
}