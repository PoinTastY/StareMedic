using StareMedic.Models;
using StareMedic.Models.Entities;
using System.Collections.ObjectModel;

namespace StareMedic.Views;

public partial class PickPatientView : ContentPage
{
    private int listpage;
    private readonly ObservableCollection<Patient> patients = new();
    public PickPatientView()
	{
        InitializeComponent();
        listpage = 1;
        BtnPrevListPage.IsEnabled = false;
        foreach (Patient patient in MainRepo.GetPatients(50,listpage))
        {
            patients.Add(patient);
        }
        if(patients.Count < 50)
        {
            BtnNextListPage.IsEnabled = false;
        }
        //implement sort later

        ListViewPatients.ItemsSource = patients.OrderBy(p => p.Nombre);
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

    [Obsolete]
    private void ListViewPatients_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem != null)
        {
            var selectedPatient = (Patient)e.SelectedItem;
            SelectedPatientLabel.Text = selectedPatient.Nombre;         
            BtnConfirmar.IsEnabled = true;
            BtnConfirmar.TextColor = Color.FromHex("#FFFBF5");
            BtnConfirmar.BackgroundColor = Color.FromHex("#7743DB");
        }
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
        ListViewPatients.ItemsSource = MainRepo.SearchPatient(SearchbarPatient.Text.ToUpper());
        BtnConfirmar.IsEnabled = false;
    }

    private void BtnPrevListPage_Clicked(object sender, EventArgs e)
    {
        if (listpage == 1)
            return;

        patients.Clear();

        var nextPatients = MainRepo.GetPatients(50, --listpage);
        
        foreach (Patient patient in nextPatients)
        {
            patients.Add(patient);
        }
        
        ListViewPatients.ItemsSource = patients.OrderBy(p => p.Nombre);
    }

    private void BtnNextListPage_Clicked(object sender, EventArgs e)
    {
        var nextPatients = MainRepo.GetPatients(50, ++listpage);
        if (nextPatients.Count < 50)
        {
            BtnNextListPage.IsEnabled = false;
            if (nextPatients.Count == 0)
                return;
        }

        patients.Clear();
        foreach (Patient patient in nextPatients)
        {
            patients.Add(patient);
        }
        BtnPrevListPage.IsEnabled = true;
        ListViewPatients.ItemsSource = patients.OrderBy(p => p.Nombre);

    }
}