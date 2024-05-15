namespace StareMedic.Views;

using System.Collections.ObjectModel;
using StareMedic.Models;
using StareMedic.Models.Entities;

public partial class SearchCC : ContentPage
{
    private readonly ObservableCollection<CCwPatient> casos = new();

    public SearchCC()
    {
        InitializeComponent();

        //default search method
        PickerFilterSearch.SelectedItem = "Paciente";
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        casos.Clear();
        foreach (CasoClinico caso in MainRepo.GetCasos())
        {
            CCwPatient vista = new(caso.IdDB, caso.Id, caso.Nombre, caso.Paciente().Nombre);
            casos.Add(vista);
        }
        ListViewCC.ItemsSource = casos;
    }


    private async void ListViewCC_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (ListViewCC.SelectedItem != null)
        {
            //It will send to de detailed patient view, or the edit pg, rn is edit?
            //Shell.Current.GoToAsync($"{nameof(ViewClinicalCase)}?Id={((CasoClinico)ListViewCC.SelectedItem).Id}");
            CCwPatient element = ListViewCC.SelectedItem as CCwPatient;
            await Navigation.PushAsync(new ViewClinicalCase(MainRepo.GetCasoById(element.Iddb)));
            //maybe directly to edit page, but ill add a button to enable edit
        }
    }

    private void ListViewCC_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        ListViewCC.SelectedItem = null;
    }

    private void SearchBarentry_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(SearchBarentry.Text))
        {
            ListViewCC.ItemsSource = casos;
        }
    }

    private void SearchBarentry_SearchButtonPressed(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(SearchBarentry.Text))
        {
            if (PickerFilterSearch.SelectedItem.ToString() == "Paciente")
            {
                ListViewCC.ItemsSource = casos.Where(caso => caso.PatientName.Contains(SearchBarentry.Text.ToUpper()));
            }
            else if(PickerFilterSearch.SelectedItem.ToString() == "Nombre")
            {
                ListViewCC.ItemsSource = casos.Where(caso => caso.Nombre.Contains(SearchBarentry.Text.ToUpper()));
            }
            else//implicit "Id"
            {
                ListViewCC.ItemsSource = casos.Where(caso => caso.Id.Contains(SearchBarentry.Text.ToUpper()));
            }
        }
    }

    private async void BtnNewCC_Clicked(object sender, EventArgs e)
    {
        BtnNewCC.Opacity = 0;
        await BtnNewCC.FadeTo(1, 200);

        await Shell.Current.GoToAsync(nameof(RegisterClinicalCase));
    }

    private async void BtnCancel_Clicked(object sender, EventArgs e)
    {
        btnCancel.Opacity = 0;
        await btnCancel.FadeTo(1, 300);

        await Shell.Current.GoToAsync("..");

    }
}