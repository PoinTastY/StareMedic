using StareMedic.Models;
using System.Collections.ObjectModel;
using StareMedic.Views.Viewers;

namespace StareMedic.Views;

using Patient = Models.Entities.Patient; //this is to declare what are we exactly referring to, if theres any conflict

public partial class Pacientes : ContentPage
{
	ObservableCollection<Patient> patients = new();
	public Pacientes()
	{
		InitializeComponent();
		foreach (Patient patient in MainRepo.GetPatients())
		{
            patients.Add(patient);
        }

        //implement sort first

		listPatients.ItemsSource = patients;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        patients.Clear();
        foreach (Patient patient in MainRepo.GetPatients())
        {
            patients.Add(patient);
        }
        listPatients.ItemsSource = patients;
        

    }

    private async void BtnCancel_Clicked(object sender, EventArgs e)
    {
        btnCancel.Opacity = 0;
        await btnCancel.FadeTo(1, 300);

		await Shell.Current.GoToAsync("..");

    }

    private async void ListPatients_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
		if(listPatients.SelectedItem != null)
		{
			//It will send to de detailed patient view, or the edit pg, rn is edit?
			//maybe directly to edit page, but ill add a button to enable edit
            await Navigation.PushAsync(new PatientControll((Patient)listPatients.SelectedItem));
		}
    }

    private void ListPatients_ItemTapped(object sender, ItemTappedEventArgs e)
    {
		listPatients.SelectedItem = null;
    }

    private void SearchBarPatients_SearchButtonPressed(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(SearchBarPatients.Text))
        {
       
            listPatients.ItemsSource = patients.Where(patient => patient.Nombre.Contains(SearchBarPatients.Text.ToUpper()));
            
          
        }
    }

    private void SearchBarPatients_TextChanged(object sender, TextChangedEventArgs e)
    {
        if(string.IsNullOrWhiteSpace(SearchBarPatients.Text))
        {
            listPatients.ItemsSource = patients;
        }
    }

    private async void BtnAddPatient_Clicked(object sender, EventArgs e)
    {
        BtnAddPatient.Opacity = 0;
        await BtnAddPatient.FadeTo(1, 200);

        await Navigation.PushAsync(new PatientControll(null));
    }
}