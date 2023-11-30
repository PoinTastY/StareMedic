using StareMedic.Models;
using StareMedic.Models.Entities;

namespace StareMedic.Views;

public partial class RegisterMedic : ContentPage
{
    //Medic model instance
    readonly Medic medic = new(MainRepo.GetCurrentMedicIndex());

	public RegisterMedic()
	{
		InitializeComponent();
		lblID.Text = "ID: " + medic.Id.ToString();
	}

    private void EntryName_TextChanged(object sender, TextChangedEventArgs e)
    {
        medic.Nombre = EntryName.Text;
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
            medic.Telefono = EntryTelefono.Text;
        }
    }

    private async void BtnGuardar_Clicked(object sender, EventArgs e)
    {
        if(medic.Nombre != null && medic.Nombre != "")
        {
            bool confirm = await DisplayAlert("Confirmar", $"Se registrara al Medico:\n{medic.Nombre}", "Cancelar", "Confirmar");
            if (!confirm)
            {
                MainRepo.AddMedic(medic);
                //create a method to confirm directly in the database, return a bool and make an if 4 below
                await DisplayAlert("Confirmado", $"Se ha registrado al Medico:\n{medic.Nombre}", "Ok");
                await Shell.Current.GoToAsync("..");
            }
        }else
        {
            await DisplayAlert("Error", "El nombre no puede estar vacio", "Ok");
        }
    }

    private async void BtnCancel_Clicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Cancelar", "Desea cancelar el registro?", "No", "Si");
        if(!confirm)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}