namespace StareMedic.Views;

using StareMedic.Models;
using System.Collections.ObjectModel;

/* Cambio no fusionado mediante combinación del proyecto 'StareMedic (net7.0-android)'
Antes:
using Patient = StareMedic.Models.Patient; //this is to declare what are we exactly referring to, if theres any conflict
Después:
using Patient = Patient; //this is to declare what are we exactly referring to, if theres any conflict
*/
using Patient = Models.Entities.Patient; //this is to declare what are we exactly referring to, if theres any conflict

public partial class Pacientes : ContentPage
{
	ObservableCollection<Patient> patients = new();
	public Pacientes()
	{
		InitializeComponent();
		foreach (Patient patient in MainRepo.GetPatients())
		{
            patients.Insert(0, patient);
        }
		listPatients.ItemsSource = patients;
	}

    protected override void OnAppearing()
    {
        PickerFilter.SelectedIndex = 0;
    }

    private void BtnCancel_Clicked(object sender, EventArgs e)
    {
		Shell.Current.GoToAsync("..");

    }

    private async void ListPatients_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
		if(listPatients.SelectedItem != null)
		{
			//It will send to de detailed patient view, or the edit pg, rn is edit?
			await Shell.Current.GoToAsync($"{nameof(EditPatient)}?Id={((Patient)listPatients.SelectedItem).Id}");
			//maybe directly to edit page, but ill add a button to enable edit
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
            if (PickerFilter.SelectedIndex == 0)
            {
                listPatients.ItemsSource = patients.Where(patient => patient.Nombre.Contains(SearchBarPatients.Text.ToUpper()));
            }
            else
            {
                listPatients.ItemsSource = patients.Where(patient => patient.Id.ToString().Contains(SearchBarPatients.Text.ToUpper()));
            }
        }
    }

    private void SearchBarPatients_TextChanged(object sender, TextChangedEventArgs e)
    {
        if(string.IsNullOrWhiteSpace(SearchBarPatients.Text))
        {
            listPatients.ItemsSource = patients;
        }
    }
}