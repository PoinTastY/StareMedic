using StareMedic.Models;
using StareMedic.Models.Entities;

namespace StareMedic.Views.Viewers;

public partial class RoomControll : ContentPage
{
    Rooms room;
    public RoomControll(Rooms room)
    {
        InitializeComponent();
        if (room != null)
        {

            this.room = room;
            EntryName.Text = room.Nombre;
            EditorDescripcion.Text = room.Descripcion;
            EnableDisable(false);
        }
        else
        {
            BtnDelete.IsVisible = false;
            BtnDelete.IsEnabled = false;
            this.room = new(MainRepo.GetCurrentRoomIndex());
        }
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
        BtnGuardar.Opacity = 0;

        await BtnGuardar.FadeTo(1, 300);

        if (room.Nombre != null && room.Nombre != "")
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
        BtnCancel.Opacity = 0;

        await BtnCancel.FadeTo(1, 300);

        bool confirm = await DisplayAlert("Cancelar", "Desea cancelar el registro?", "No", "Si");
        if (!confirm)
        {
            await DisplayAlert("Cancelado", "No se ha realizado ningun cambio", "Ok");
            await Shell.Current.GoToAsync("..");
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

        bool confirm = await DisplayAlert("Eliminar", $"Desea eliminar la habitacion {room.Nombre}?", "No", "Si");
        if (!confirm)
        {
            MainRepo.DeleteRoom(room.Id);
            await DisplayAlert("Exito", $"Se elimino la habitacion {room.Nombre}", "OK");
            await Shell.Current.GoToAsync("..");
        }
    }
    private void EnableDisable(bool x)
    {
        BtnEdit.IsVisible = !x;
        BtnEdit.IsEnabled = !x;
        BtnDelete.IsVisible = !x;
        BtnDelete.IsEnabled = !x;
        EntryName.IsEnabled = x;
        EditorDescripcion.IsEnabled = x;
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
}