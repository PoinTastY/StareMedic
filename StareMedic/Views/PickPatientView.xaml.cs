using Microsoft.Maui.Graphics.Text;
using Microsoft.UI.Xaml.Media.Animation;
using StareMedic.Models;
using StareMedic.Models.Entities;
using System.Collections.ObjectModel;
using Windows.Devices.Display.Core;

namespace StareMedic.Views;

public class PatientSelectedEventArgs : EventArgs
{
    public Patient SelectedPatient { get; set; }
}

public partial class PickPatientView : ContentPage
{
    private int listpage;
    private readonly ObservableCollection<Patient> patients = new();

    public event EventHandler<PatientSelectedEventArgs> PatientSelected;

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
        BtnConfirmar.IsEnabled = false;
        ListViewPatients.ItemsSource = patients.OrderBy(p => p.Nombre);
	}

    private async void BtnConfirmar_Clicked(object sender, EventArgs e)
    {
        BtnConfirmar.Opacity = 0;
        await BtnConfirmar.FadeTo(1, 200);

        Patient paciente = (Patient)ListViewPatients.SelectedItem;

        //u sure?
        bool confirmacion = await DisplayAlert("Confirmacion", $"Seleccionar a: {paciente.Nombre}?","Volver", "Confirmar");


        
        if (!confirmacion)
        {
            try
            {
                PatientSelected?.Invoke(this, new PatientSelectedEventArgs { SelectedPatient = paciente });
                //await Navigation.PopModalAsync(); // esta en el evento mejor
            }
            catch
            {
                await DisplayAlert("Error", "Algo salio mal con el evento de seleccion.", "Volver");
                await Navigation.PopModalAsync();
            }
        }
        else
        {
            await DisplayAlert("Error", "Se ha cancelado la eleccion del paciente.", "Ok");
            await Navigation.PopModalAsync();
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

        patients.Clear();

        var nextPatients = MainRepo.GetPatients(50, --listpage);
        
        foreach (Patient patient in nextPatients)
        {
            patients.Add(patient);
        }
        
        ListViewPatients.ItemsSource = patients.OrderBy(p => p.Nombre);

        if (listpage == 1)
        {
            BtnPrevListPage.IsEnabled = false;
            BtnNextListPage.IsEnabled = true;
            return;
        }
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