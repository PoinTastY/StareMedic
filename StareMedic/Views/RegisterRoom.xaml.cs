using StareMedic.Models;
using StareMedic.Models.Entities;

namespace StareMedic.Views;

public partial class RegisterRoom : ContentPage
{
    readonly Rooms room = new(MainRepo.GetCurrentRoomIndex());
	public RegisterRoom()
	{
		InitializeComponent();
        lblID.Text = "ID: " + room.Id.ToString();
	}

    private void EntryName_TextChanged(object sender, TextChangedEventArgs e)
    {
        room.Nombre = e.NewTextValue;
    }

    private void EditorDescripcion_TextChanged(object sender, TextChangedEventArgs e)
    {
        room.Descripcion = e.NewTextValue;
    }

    private async void BtnGuardar_Clicked(object sender, EventArgs e)
    {
        if(room.Nombre != null && room.Nombre != "")
        {
            bool confirmacion = await DisplayAlert("Confirmacion", $"Habitacion a Registrar:\n{room.Nombre}", "Cancelar", "Confirmar");
            if (!confirmacion)
            {
                MainRepo.AddRoom(room);
                await DisplayAlert("Exito", $"Se registrio la habitacion:\n{room.Nombre}", "OK");
                await Shell.Current.GoToAsync("..");
            }
        }
        else
        {
            await DisplayAlert("Error:", "No se puede registrar nada sin un nombre", "OK");
        }


    }

    private async void BtnCancel_Clicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Cancelar", "Desea cancelar el registro?", "No", "Si");
        if (!confirm)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}