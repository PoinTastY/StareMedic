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
            patients.Add(patient);
        }

        //implement sort later

        ListViewPatients.ItemsSource = patients;
	}

    private async void BtnConfirmar_Clicked(object sender, EventArgs e)
    {
        BtnConfirmar.Opacity = 0;
        await BtnConfirmar.FadeTo(1, 200);

        MainRepo.PatientIdSolver = (Patient)ListViewPatients.SelectedItem;
        //implement R U Sure?
        bool confirmacion = await DisplayAlert("Confirmacion", $"Seleccionar a: {MainRepo.PatientIdSolver.Nombre}?","Volver", "Confirmar");
        
        if (!confirmacion)
        {
            //this if works as a try lol
            if (MainRepo.PatientIdSolver != null)
            {
                await Navigation.PopModalAsync();
            }
            else
            {
                await DisplayAlert("Error", "Parece que el paciente ya esta en un caso clinico", "Ok");
                ListViewPatients.SelectedItem = null;
                BtnConfirmar.IsEnabled = false;
            }
        }
    }

    private async void BtnCancel_Clicked(object sender, EventArgs e)
    {
        BtnCancel.Opacity = 0;
        await BtnCancel.FadeTo(1, 200);

        await Navigation.PopModalAsync();
    }

    private void ListViewPatients_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        BtnConfirmar.IsEnabled = true;
    }

    private void SearchbarPatient_TextChanged(object sender, TextChangedEventArgs e)
    {
        if(string.IsNullOrWhiteSpace(SearchbarPatient.Text))
        {
            ListViewPatients.ItemsSource = patients;
            BtnConfirmar.IsEnabled = false;
        }
    }

    private void SearchbarPatient_SearchButtonPressed(object sender, EventArgs e)
    {
        ListViewPatients.ItemsSource = patients.Where(patient => patient.Nombre.Contains(SearchbarPatient.Text.ToUpper()));
        BtnConfirmar.IsEnabled = false;
    }
}