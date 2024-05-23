namespace StareMedic.Views;

using System.Collections.ObjectModel;
using StareMedic.Models;
using StareMedic.Models.Entities;

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

    protected override void OnAppearing()
    {
        base.OnAppearing();
        casos.Clear();
        var listCasos = MainRepo.GetCasos(50, listpage);
        foreach (CasoClinico caso in listCasos)
        {
            CCwPatient vista = new(caso.IdDB, caso.Id, caso.Nombre, caso.Paciente().Nombre);
            casos.Add(vista);
        }
        ListViewCC.ItemsSource = casos;
        if(listpage == 1)
            BtnPrevListPage.IsEnabled = false;
        
        if (casos.Count < 50)
            BtnNextListPage.IsEnabled = false;
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

    private void SearchBarentry_TextChanged(object sender, TextChangedEventArgs e)//validate here the things we validate with the listpage
    {
        if (string.IsNullOrWhiteSpace(SearchBarentry.Text))
        {
            casos.Clear();
            foreach (CasoClinico caso in MainRepo.GetCasos(50, listpage))
            {
                CCwPatient vista = new(caso.IdDB, caso.Id, caso.Nombre, caso.Paciente().Nombre);
                casos.Add(vista);
            }
            ListViewCC.ItemsSource = casos;
            if (listpage == 1)
                BtnPrevListPage.IsEnabled = false;
            else
                BtnPrevListPage.IsEnabled = true;

            if(casos.Count < 50)
                BtnNextListPage.IsEnabled = false;
            else 
                BtnNextListPage.IsEnabled = true;

        }
    }

    private void SearchBarentry_SearchButtonPressed(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(SearchBarentry.Text))
        {
            BtnPrevListPage.IsEnabled = false;
            BtnNextListPage.IsEnabled = false;
            if (PickerFilterSearch.SelectedItem.ToString() == "Paciente")
            {
                casos.Clear();
                foreach(var caso in MainRepo.SearchCasoClinico(SearchBarentry.Text.ToUpper(), 1))
                {
                    CCwPatient vista = new(caso.IdDB, caso.Id, caso.Nombre, caso.Paciente().Nombre);
                    casos.Add(vista);
                }
                ListViewCC.ItemsSource = casos;
            }
            else if(PickerFilterSearch.SelectedItem.ToString() == "Nombre")
            {
                casos.Clear();
                foreach (var caso in MainRepo.SearchCasoClinico(SearchBarentry.Text.ToUpper(), 2))
                {
                    CCwPatient vista = new(caso.IdDB, caso.Id, caso.Nombre, caso.Paciente().Nombre);
                    casos.Add(vista);
                }
                ListViewCC.ItemsSource = casos;
            }
            else if(PickerFilterSearch.SelectedItem.ToString() == "Id")
            {
                casos.Clear();
                foreach (var caso in MainRepo.SearchCasoClinico(SearchBarentry.Text.ToUpper(), 3))
                {
                    CCwPatient vista = new(caso.IdDB, caso.Id, caso.Nombre, caso.Paciente().Nombre);
                    casos.Add(vista);
                }
                ListViewCC.ItemsSource = casos;
            }
            else//implicit "medic"
            {
                casos.Clear();
                foreach (var caso in MainRepo.SearchCasoClinico(SearchBarentry.Text.ToUpper(), 4))
                {
                    CCwPatient vista = new(caso.IdDB, caso.Id, caso.Nombre, caso.Paciente().Nombre);
                    casos.Add(vista);
                }
                ListViewCC.ItemsSource = casos;
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

    private void BtnPrevListPage_Clicked(object sender, EventArgs e)
    {
        if (listpage == 1)
        {
            BtnPrevListPage.IsEnabled = false;
            return;
        }

        casos.Clear();

        var nextCasos = MainRepo.GetCasos(50, --listpage);

        foreach (CasoClinico caso in nextCasos)
        {
            CCwPatient vista = new(caso.IdDB, caso.Id, caso.Nombre, caso.Paciente().Nombre);
            casos.Add(vista);
        }

        ListViewCC.ItemsSource = casos;
    }

    private void BtnNextListPage_Clicked(object sender, EventArgs e)
    {
        var nextCasos = MainRepo.GetCasos(50, ++listpage);
        if (nextCasos.Count < 50)
        {
            BtnNextListPage.IsEnabled = false;
            if (nextCasos.Count == 0)
                return;
        }

        casos.Clear();
        foreach (CasoClinico caso in nextCasos)
        {
            CCwPatient vista = new(caso.IdDB, caso.Id, caso.Nombre, caso.Paciente().Nombre);
            casos.Add(vista);
        }
        BtnPrevListPage.IsEnabled = true;
        ListViewCC.ItemsSource = casos;
    }
}