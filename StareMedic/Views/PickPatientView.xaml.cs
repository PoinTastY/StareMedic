using StareMedic.Models;
using StareMedic.Models.Entities;
using System.Collections.ObjectModel;

namespace StareMedic.Views;

public partial class PickPatientView : ContentPage
{
    private readonly ObservableCollection<Patient> patients = new();
    public PickPatientView()
	{

        InitializeComponent();
        foreach (Patient patient in MainRepo.GetPatients())
        {
            if (patient.Status == false)
                patients.Add(patient);
        }

        ListViewPatients.ItemsSource = patients;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        ListViewPatients.ItemsSource = patients;
    }

    private async void BtnConfirmar_Clicked(object sender, EventArgs e)
    {
        MainRepo.PatientIdSolver = (Patient)ListViewPatients.SelectedItem;
        if (MainRepo.PatientIdSolver.Status == false && MainRepo.PatientIdSolver != null) 
        {
            await Navigation.PopModalAsync();
        }
        else
        {
            await DisplayAlert("Error", "El paciente ya esta en un caso clinico", "Ok");
            ListViewPatients.SelectedItem = null;
            BtnConfirmar.IsEnabled = false;
        }
    }

    private async void BtnCancel_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    private void ListViewPatients_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        BtnConfirmar.IsEnabled = true;
    }

    private void EntrySearch_TextChanged(object sender, TextChangedEventArgs e)
    {
        //search by name in patients list
        var filteredPatients = MainRepo.GetPatients().Where(patient => patient.Nombre.Contains(e.NewTextValue));
        patients.Clear();
        foreach (Patient patient in filteredPatients)
        {
            if (patient.Status == false)
                patients.Add(patient);
        }
    }
}