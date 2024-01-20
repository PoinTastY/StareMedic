namespace StareMedic.Views.Viewers;
using StareMedic.Models.Entities;
using StareMedic.Models;
using StareMedic.Views.Viewers;

public partial class MedicControll : ContentPage
{
	private Medic doctor;
	public MedicControll(Medic doctor)
	{
		InitializeComponent();
        if(doctor.Nombre != "missing")
        {
            this.doctor = doctor;
            EntryName.Text = doctor.Nombre;
            EntryTelefono.Text = doctor.Telefono;
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
            await Navigation.PopAsync();
        }
    }
}