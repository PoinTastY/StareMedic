namespace StareMedic.Views;

using CommunityToolkit.Maui.Views;
using StareMedic.Models;
using StareMedic.Models.Entities;
using StareMedic.Views.Viewers;
using System.Collections.ObjectModel;

public partial class SearchCC : ContentPage
{
    private readonly ObservableCollection<CCwPatient> casos = new();
    private int listpage;
    public SearchCC()
    {
        InitializeComponent();
        listpage = 1;
        //default search method
        PickerFilterSearch.SelectedItem = "Paciente";
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        casos.Clear();
        listpage = 1;
        var listCasos = await MainRepo.GetCasosAsync(50, listpage);//maybe get a temporal list to optimize this
        foreach (CasoClinico caso in listCasos)
        {
            CCwPatient vista = new(caso, caso.Paciente(), caso.Medico());
            casos.Add(vista);
        }
        ListViewCC.ItemsSource = casos;
        if (listpage == 1)
            BtnPrevListPage.IsEnabled = false;

        if (casos.Count < 50)
            BtnNextListPage.IsEnabled = false;
        else
            BtnNextListPage.IsEnabled = true;
    }


    private async void ListViewCC_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (ListViewCC.SelectedItem != null)
        {
            var popup = new SpinnerPopup();
            this.ShowPopup(popup);
            try
            {
                CCwPatient element = ListViewCC.SelectedItem as CCwPatient;
                await Navigation.PushAsync(new ViewClinicalCase(MainRepo.GetCasoById(element.Iddb)));
            }
            finally
            {
                popup.Close();
            }
        }
    }

    private void ListViewCC_ItemTapped(object sender, ItemTappedEventArgs e)
    {

        ListViewCC.SelectedItem = null;
    }

    private async void SearchBarentry_TextChanged(object sender, TextChangedEventArgs e)//validate here the things we validate with the listpage
    {
        if (string.IsNullOrWhiteSpace(SearchBarentry.Text))
        {
            var popup = new SpinnerPopup();
            this.ShowPopup(popup);
            try
            {
                casos.Clear();
                foreach (CasoClinico caso in await MainRepo.GetCasosAsync(50, listpage))
                {
                    CCwPatient vista = new(caso, caso.Paciente(), caso.Medico());
                    casos.Add(vista);
                }
                ListViewCC.ItemsSource = casos;
                if (listpage == 1)
                    BtnPrevListPage.IsEnabled = false;
                else
                    BtnPrevListPage.IsEnabled = true;

                if (casos.Count < 50)
                    BtnNextListPage.IsEnabled = false;
                else
                    BtnNextListPage.IsEnabled = true;
            }
            finally
            {
                popup.Close();
            }
        }
    }

    private void SearchBarentry_SearchButtonPressed(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(SearchBarentry.Text))
        {

            var popup = new SpinnerPopup();
            this.ShowPopup(popup);
            try
            {
                BtnPrevListPage.IsEnabled = false;
                BtnNextListPage.IsEnabled = false;
                if (PickerFilterSearch.SelectedItem.ToString() == "Paciente")
                {
                    casos.Clear();
                    foreach (var caso in MainRepo.SearchCasoClinico(SearchBarentry.Text.ToUpper(), 1))
                    {
                        CCwPatient vista = new(caso, caso.Paciente(), caso.Medico());
                        casos.Add(vista);
                    }
                    ListViewCC.ItemsSource = casos;
                }
                else if (PickerFilterSearch.SelectedItem.ToString() == "Nombre")
                {
                    casos.Clear();
                    foreach (var caso in MainRepo.SearchCasoClinico(SearchBarentry.Text.ToUpper(), 2))
                    {
                        CCwPatient vista = new(caso, caso.Paciente(), caso.Medico());
                        casos.Add(vista);
                    }
                    ListViewCC.ItemsSource = casos;
                }
                else if (PickerFilterSearch.SelectedItem.ToString() == "Id")
                {
                    casos.Clear();
                    foreach (var caso in MainRepo.SearchCasoClinico(SearchBarentry.Text.ToUpper(), 3))
                    {
                        CCwPatient vista = new(caso, caso.Paciente(), caso.Medico());
                        casos.Add(vista);
                    }
                    ListViewCC.ItemsSource = casos;
                }
                else//implicit "medic"
                {
                    casos.Clear();
                    foreach (var caso in MainRepo.SearchCasoClinico(SearchBarentry.Text.ToUpper(), 4))
                    {
                        CCwPatient vista = new(caso, caso.Paciente(), caso.Medico());
                        casos.Add(vista);
                    }
                    ListViewCC.ItemsSource = casos;
                }
            }
            finally
            {
                popup.Close();
            }
        }
    }

    private async void BtnNewCC_Clicked(object sender, EventArgs e)
    {
        var popup = new SpinnerPopup();
        this.ShowPopup(popup);
        try
        {
            BtnNewCC.Opacity = 0;
            await BtnNewCC.FadeTo(1, 200);

            await Shell.Current.GoToAsync(nameof(RegisterClinicalCase));
        }
        finally
        {
            popup.Close();
        }
    }

    private async void BtnCancel_Clicked(object sender, EventArgs e)
    {
        var popup = new SpinnerPopup();
        this.ShowPopup(popup);
        try
        {
            btnCancel.Opacity = 0;
            await btnCancel.FadeTo(1, 300);

            await Shell.Current.GoToAsync("..");
        }
        finally
        {
            popup.Close();
        }
    }

    private async void BtnPrevListPage_Clicked(object sender, EventArgs e)
    {
        var popup = new SpinnerPopup();
        this.ShowPopup(popup);
        try
        {
            BtnPrevListPage.Opacity = 0;

            await BtnPrevListPage.FadeTo(1, 300);

            casos.Clear();

            var nextCasos = await MainRepo.GetCasosAsync(50, --listpage);

            foreach (CasoClinico caso in nextCasos)
            {
                CCwPatient vista = new(caso, caso.Paciente(), caso.Medico());
                casos.Add(vista);
            }

            ListViewCC.ItemsSource = casos;
            if (listpage == 1)
            {
                BtnPrevListPage.IsEnabled = false;
                BtnNextListPage.IsEnabled = true;
                return;
            }
        }
        finally
        {
            if (listpage == 1)
                BtnPrevListPage.IsEnabled = false;
            else
                BtnPrevListPage.IsEnabled = true;
            popup.Close();
        }
    }

    private async void BtnNextListPage_Clicked(object sender, EventArgs e)
    {
        var popup = new SpinnerPopup();
        this.ShowPopup(popup);
        try
        {
            BtnNextListPage.Opacity = 0;

            await BtnNextListPage.FadeTo(1, 300);
            var nextCasos = await MainRepo.GetCasosAsync(50, ++listpage);
            if (nextCasos.Count < 50)
            {
                BtnNextListPage.IsEnabled = false;
                if (nextCasos.Count == 0)
                    return;


            }


            casos.Clear();
            foreach (CasoClinico caso in nextCasos)
            {
                CCwPatient vista = new(caso, caso.Paciente(), caso.Medico());
                casos.Add(vista);
            }

            BtnPrevListPage.IsEnabled = true;
            ListViewCC.ItemsSource = casos;

        }
        finally
        {
            if (casos.Count < 50)
                BtnNextListPage.IsEnabled = false;
            else
                BtnNextListPage.IsEnabled = true;
            popup.Close();
        }
    }
}