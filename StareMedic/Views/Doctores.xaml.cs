using System.Collections.ObjectModel;
using StareMedic.Models.Entities;
using StareMedic.Models;
using StareMedic.Views.Viewers;
using CommunityToolkit.Maui.Views;

namespace StareMedic.Views;

public partial class Doctores : ContentPage
{

	ObservableCollection<Medic> medics = new();

    int listpage;
	public Doctores()
	{
		InitializeComponent();
        listpage = 1;
        BtnPrevListPage.IsEnabled = false;

    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        medics.Clear();
        foreach (Medic medic in MainRepo.GetMedicsLight(listpage, 50))
        {
            medics.Add(medic);
        }
        listMedics.ItemsSource = medics.OrderBy(m => m.Id);
        if (medics.Count < 50)
        {
            BtnNextListPage.IsEnabled = false;
        }

        //var popup = new SpinnerPopup();
        //this.ShowPopup(popup);
        //try
        //{
            

        //}
        //finally
        //{
        //    popup.Close();
        //}
    }
        



    private async void listMedics_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if(listMedics.SelectedItem != null)
        {
            var popup = new SpinnerPopup();
            this.ShowPopup(popup);
            try
            {
                await Navigation.PushAsync(new MedicControll((Medic)listMedics.SelectedItem));
            }
            finally { popup.Close(); }
        }
    }

    private void listMedics_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        listMedics.SelectedItem = null;
    }

    private void SearchBarMedics_SearchButtonPressed(object sender, EventArgs e)
    {
        if(!string.IsNullOrWhiteSpace(SearchBarMedics.Text))
        {
            var popup = new SpinnerPopup();
            this.ShowPopup(popup);
            try
            {
                medics.Clear();
                foreach (var medic in MainRepo.SearchMedic(SearchBarMedics.Text.ToUpper()))
                {
                    medics.Add(medic);
                }
                listMedics.ItemsSource = medics.OrderBy(m => m.Id);
            }
            finally
            {
                popup.Close();
            }
        }
    }

    private void SearchBarMedics_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(SearchBarMedics.Text))
        {
            var popup = new SpinnerPopup();
            this.ShowPopup(popup);
            try
            {
                medics.Clear();
                foreach (Medic medic in MainRepo.GetMedicsLight(listpage, 50))
                {
                    medics.Add(medic);
                }
                listMedics.ItemsSource = medics;
            }
            finally
            {
                popup.Close();
            }
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
        var popup = new SpinnerPopup();
        this.ShowPopup(popup);
        try
        {
            await Navigation.PushAsync(new MedicControll(null));
        }
        finally
        {
            popup.Close();
        }
    }

    private void BtnPrevListPage_Clicked(object sender, EventArgs e)
    {

        var popup = new SpinnerPopup();
        this.ShowPopup(popup);
        try
        {
            medics.Clear();

            var nextMedics = MainRepo.GetMedicsLight(50, --listpage);

            foreach (Medic medic in nextMedics)
            {
                medics.Add(medic);
            }

            listMedics.ItemsSource = medics.OrderBy(p => p.Nombre);
            if (listpage == 1)
            {
                BtnPrevListPage.IsEnabled = false;
                BtnNextListPage.IsEnabled = true;
                return;
            }
        }
        finally
        {
            popup.Close();
        }
    }

    private void BtnNextListPage_Clicked(object sender, EventArgs e)
    {
        var popup = new SpinnerPopup();
        this.ShowPopup(popup);
        try
        {
            var nextMedics = MainRepo.GetMedicsLight(50, ++listpage);
            if (nextMedics.Count < 50)
            {
                BtnNextListPage.IsEnabled = false;
                if (nextMedics.Count == 0)
                    return;
            }

            medics.Clear();
            foreach (Medic medic in nextMedics)
            {
                medics.Add(medic);
            }
            BtnPrevListPage.IsEnabled = true;
            listMedics.ItemsSource = medics.OrderBy(p => p.Nombre);
        }
        finally
        {
            popup.Close();
        }
    }
}