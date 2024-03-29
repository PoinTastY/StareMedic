namespace StareMedic.Views.Viewers;
using StareMedic.Models.Entities;
using StareMedic.Models;

public partial class MedicControll : ContentPage
{
	private Medic doctor;
	public MedicControll(Medic doctor)
	{
		InitializeComponent();
        if(doctor != null)
        {
            this.doctor = doctor;
            EntryName.Text = doctor.Nombre;
            EntryTelefono.Text = doctor.Telefono;
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

    private async void BtnGuardar_Clicked(object sender, EventArgs e)
    {
        if (doctor.Nombre != null && doctor.Nombre != "")
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
            await DisplayAlert("Error", "El nombre no puede estar vacio", "Ok");
        }
    }

    private async void BtnCancel_Clicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Cancelar", "Desea cancelar el registro?", "No", "Si");
        if (!confirm)
        {
            await DisplayAlert("Cancelado", "No se ha realizado ningun cambio", "Ok");
            await Navigation.PopAsync();
        }
    }

    private async void BtnDelete_Clicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Eliminar", $"Desea eliminar al Doctor: {doctor.Nombre}?", "No", "Si");
        if (!confirm)
        {
            MainRepo.DeleteMedic(doctor.Id);
            await DisplayAlert("Exito", $"Medico eliminado: {doctor.Nombre}", "OK");
            await Shell.Current.GoToAsync("..");
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
        BtnCancel.IsVisible = x;
        BtnCancel.IsEnabled = x;
        BtnGuardar.IsVisible = x;
        BtnGuardar.IsEnabled = x;
    }

    private void BtnEdit_Clicked(object sender, EventArgs e)
    {
        EnableDisable(true);
    }
}