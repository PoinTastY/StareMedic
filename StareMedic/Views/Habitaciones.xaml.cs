using StareMedic.Models;
using StareMedic.Views.Viewers;
using System.Collections.ObjectModel;

namespace StareMedic.Views;

using Rooms = Models.Entities.Rooms;

public partial class Habitaciones : ContentPage
{
    ObservableCollection<Rooms> rooms = new();
    public Habitaciones()
    {
        InitializeComponent();
        foreach (Rooms room in MainRepo.GetRooms())
        {
            rooms.Add(room);
        }

        ListRooms.ItemsSource = rooms;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        rooms.Clear();
        foreach (Rooms room in MainRepo.GetRooms())
        {
            rooms.Add(room);
        }
        ListRooms.ItemsSource = rooms;
    }

    private void ListRooms_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (ListRooms.SelectedItem != null)
        {
            //go to room controll
            Navigation.PushAsync(new RoomControll((Rooms)ListRooms.SelectedItem));
        }
    }

    private void ListRooms_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        ListRooms.SelectedItem = null;
    }

    private void SearchBarRooms_SearchButtonPressed(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(SearchBarRooms.Text))
        {
            ListRooms.ItemsSource = rooms.Where(room => room.Nombre.Contains(SearchBarRooms.Text.ToUpper()));
        }
        else
        {
            ListRooms.ItemsSource = rooms;
        }
    }

    private void SearchBarRooms_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(SearchBarRooms.Text))
        {
            ListRooms.ItemsSource = rooms;
        }
    }

    private async void BtnAddRoom_Clicked(object sender, EventArgs e)
    {
        BtnAddRoom.Opacity = 0;
        await BtnAddRoom.FadeTo(1, 300);

        await Navigation.PushAsync(new RoomControll(null));
    }

    private async void btnCancel_Clicked(object sender, EventArgs e)
    {
        btnCancel.Opacity = 0;
        await btnCancel.FadeTo(1, 300);

        await Shell.Current.GoToAsync("..");
    }
}