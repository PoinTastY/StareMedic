using System.Collections.ObjectModel;
using StareMedic.Models.Entities;
using StareMedic.Models;
using StareMedic.Views.Viewers;

namespace StareMedic.Views;

public partial class Doctores : ContentPage
{

	ObservableCollection<Medic> medics = new();

	public Doctores()
	{
		InitializeComponent();
        foreach (Medic medic in MainRepo.GetMedics())
        {
            medics.Add(medic);
        }

        listMedics.ItemsSource = medics;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        medics.Clear();
        foreach (Medic medic in MainRepo.GetMedics())
        {
            medics.Add(medic);
        }
        listMedics.ItemsSource = medics;
    }


    private async void listMedics_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if(listMedics.SelectedItem != null)
        {
            await Navigation.PushAsync(new MedicControll((Medic)listMedics.SelectedItem));
        }
    }

    private void listMedics_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        listMedics.SelectedItem = null;
    }

    private void SearchBarMedics_SearchButtonPressed(object sender, EventArgs e)
    {
        listMedics.ItemsSource = medics.Where(medic => medic.Nombre.Contains(SearchBarMedics.Text.ToUpper()));
    }

    private void SearchBarMedics_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(SearchBarMedics.Text))
        {
            listMedics.ItemsSource = medics;
        }
    }

    private async void btnCancel_Clicked(object sender, EventArgs e)
    {
        btnCancel.Opacity = 0;
        await btnCancel.FadeTo(1, 300);

        await Shell.Current.GoToAsync("..");
    }

    private async void BtnAddMedic_Clicked(object sender, EventArgs e)
    {
        BtnAddMedic.Opacity = 0;
        await BtnAddMedic.FadeTo(1, 300);

        await  Navigation.PushAsync(new MedicControll(null));
    }
}