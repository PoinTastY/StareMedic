namespace StareMedic.Views;

using System.Collections.ObjectModel;
using StareMedic.Models;
using StareMedic.Models.Entities;


public partial class SearchCC : ContentPage
{
    private readonly ObservableCollection<CasoClinico> casos = new();
    private readonly List<CasoClinico> cases = MainRepo.GetCasos();
            
    protected bool onlyActives = true;
    public SearchCC()
	{
		InitializeComponent();
        foreach (CasoClinico caso in MainRepo.GetCasos())
        {
            if (onlyActives)
            {
                if(!caso.Activo)
                {
                    continue;
                }
                casos.Add(caso);

            }
            else
            {
                casos.Add(caso);
            }
        }

        ListViewCC.ItemsSource = casos;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        var filteredPatients = casos.Where(caso => caso.Activo == onlyActives);
        filteredPatients = filteredPatients.ToList();
        casos.Clear();
        foreach (CasoClinico caso in filteredPatients)
        {
            casos.Add(caso);
        }
    }


    private async void ListViewCC_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if(ListViewCC.SelectedItem != null)
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

    private void EntrySearch_TextChanged(object sender, TextChangedEventArgs e)
    {//maybe, for efficiency, would have not to be reloading coincidences, just search and show
        //filtering the list
        if (SwtchIdorName.IsToggled)
        {
            var filteredPatients = cases.Where(caso => caso.Nombre.Contains(e.NewTextValue));
            filteredPatients = filteredPatients.ToList();
            casos.Clear();
            foreach (CasoClinico caso in filteredPatients)
            {
                casos.Add(caso);
            }
        }
        else
        {
            var filteredPatients = cases.Where(caso => caso.Id.Contains(e.NewTextValue));
            filteredPatients = filteredPatients.ToList();
            casos.Clear();
            foreach (CasoClinico caso in filteredPatients)
            {
                casos.Add(caso);
            }
        }
        
        
    }

    private void ChkBxOnlyActives_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value)
        {
            var filteredPatients = cases.Where(caso => caso.Activo == e.Value);
            filteredPatients = filteredPatients.ToList();
            casos.Clear();
            foreach (CasoClinico caso in filteredPatients)
            {
                casos.Add(caso);
            }
        }
        onlyActives = e.Value;
    }

    private void SwtchIdorName_Toggled(object sender, ToggledEventArgs e)
    {

    }
}