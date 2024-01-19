namespace StareMedic.Views;

using System.Collections.ObjectModel;
using StareMedic.Models;
using StareMedic.Models.Entities;


public partial class SearchCC : ContentPage
{
    private readonly ObservableCollection<CasoClinico> casos = new();

    public SearchCC()
    {
        InitializeComponent();
        
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        casos.Clear();
        foreach (CasoClinico caso in MainRepo.GetCasos())
        {
            casos.Add(caso);
        }

        ListViewCC.ItemsSource = casos;

    }


    private async void ListViewCC_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (ListViewCC.SelectedItem != null)
        {
            //It will send to de detailed patient view, or the edit pg, rn is edit?
            //Shell.Current.GoToAsync($"{nameof(ViewClinicalCase)}?Id={((CasoClinico)ListViewCC.SelectedItem).Id}");
            await Navigation.PushAsync(new ViewClinicalCase((CasoClinico)ListViewCC.SelectedItem));
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
            if (ChkBxOnlyActives.IsChecked)
            {
                ListViewCC.ItemsSource = casos.Where(caso => caso.Activo == true);
            }
            else
            {
                ListViewCC.ItemsSource = casos;
            }
        }
    }

    

    private void SearchBarentry_SearchButtonPressed(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(SearchBarentry.Text))
        {
            if (SwtchIdorName.IsToggled)
            {
                if (ChkBxOnlyActives.IsChecked)
                {
                    ListViewCC.ItemsSource = casos.Where(caso => caso.Nombre.Contains(SearchBarentry.Text.ToUpper()) && caso.Activo == true);
                }
                else
                {
                    ListViewCC.ItemsSource = casos.Where(caso => caso.Nombre.Contains(SearchBarentry.Text.ToUpper()));
                }
            }
            else
            {
                if (ChkBxOnlyActives.IsChecked)
                {
                    ListViewCC.ItemsSource = casos.Where(caso => caso.Id.Contains(SearchBarentry.Text.ToUpper()) && caso.Activo == true);
                }
                else
                {
                    ListViewCC.ItemsSource = casos.Where(caso => caso.Id.Contains(SearchBarentry.Text.ToUpper()));
                }
            }
        }
        else
        {
            if (ChkBxOnlyActives.IsChecked)
            {
                ListViewCC.ItemsSource = casos.Where(caso => caso.Activo == true);
            }
            else
            {
                ListViewCC.ItemsSource = casos;
            }
        }
            
    }

    private void ChkBxOnlyActives_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (ChkBxOnlyActives.IsChecked)
        {
            ListViewCC.ItemsSource = casos.Where(caso => caso.Activo == true);
        }
        else
        {
            ListViewCC.ItemsSource = casos;
        }
    }
}