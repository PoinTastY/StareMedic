using CommunityToolkit.Maui.Views;
using StareMedic.Models;
using StareMedic.Views.Viewers;
using System.Collections.ObjectModel;

namespace StareMedic.Views;

using Patient = Models.Entities.Patient; //this is to declare what are we exactly referring to, if theres any conflict

public partial class Pacientes : ContentPage
{
    ObservableCollection<Patient> patients = new();
    private int listpage;
    public Pacientes()
    {
        InitializeComponent();
        listpage = 1;
        BtnPrevListPage.IsEnabled = false;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        patients.Clear();
        listpage = 1;

        foreach (Patient patient in MainRepo.GetPatients(50, listpage))
        {
            patients.Add(patient);
        }
        listPatients.ItemsSource = patients.OrderBy(p => p.Id);
        if (listpage == 1)
            BtnPrevListPage.IsEnabled = false;

        if (patients.Count < 50)
            BtnNextListPage.IsEnabled = false;
        else
            BtnNextListPage.IsEnabled = true;
    }

    private async void BtnCancel_Clicked(object sender, EventArgs e)
    {
        var popup = new SpinnerPopup();
        this.ShowPopup(popup);
        try
        {
            btnCancel.Opacity = 0;
            await btnCancel.FadeTo(1, 300);

            await Shell.Current.GoToAsync("..");
        }
        finally
        {
            popup.Close();
        }

    }

    private async void ListPatients_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {

        if (listPatients.SelectedItem != null)
        {
            var popup = new SpinnerPopup();
            this.ShowPopup(popup);
            try
            {
                Patient patient = new((Patient)listPatients.SelectedItem);
                await Navigation.PushAsync(new PatientControll(patient));
            }
            finally
            {
                popup.Close();
            }
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
            var popup = new SpinnerPopup();
            this.ShowPopup(popup);
            try
            {
                patients.Clear();
                foreach (var paciente in MainRepo.SearchPatient(SearchBarPatients.Text.ToUpper()))
                {
                    patients.Add(paciente);
                }

                listPatients.ItemsSource = patients.OrderBy(p => p.Nombre);
            }
            finally
            {
                popup.Close();
            }
        }
    }

    private void SearchBarPatients_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(SearchBarPatients.Text))
        {
            var popup = new SpinnerPopup();
            this.ShowPopup(popup);
            try
            {
                patients.Clear();

                foreach (Patient patient in MainRepo.GetPatients(50, listpage))
                {
                    patients.Add(patient);
                }
                listPatients.ItemsSource = patients.OrderBy(p => p.Nombre);
            }
            finally
            {
                popup.Close();
            }
        }
    }

    private async void BtnAddPatient_Clicked(object sender, EventArgs e)
    {
        var popup = new SpinnerPopup();
        this.ShowPopup(popup);
        try
        {
            BtnAddPatient.Opacity = 0;
            await BtnAddPatient.FadeTo(1, 200);
            await Navigation.PushAsync(new PatientControll(null));
        }
        finally
        {
            popup.Close();
        }
    }

    private void BtnPrevListPage_Clicked(object sender, EventArgs e)
    {
        var popup = new SpinnerPopup();
        this.ShowPopup(popup);
        try
        {
            patients.Clear();

            var nextPatients = MainRepo.GetPatients(50, --listpage);

            foreach (Patient patient in nextPatients)
            {
                patients.Add(patient);
            }

            listPatients.ItemsSource = patients.OrderBy(p => p.Nombre);
            if (listpage == 1)
            {
                BtnPrevListPage.IsEnabled = false;
                BtnNextListPage.IsEnabled = true;
                return;
            }
        }
        finally
        {
            if (listpage == 1)
                BtnPrevListPage.IsEnabled = false;
            else
                BtnPrevListPage.IsEnabled = true;
            popup.Close();
        }
    }

    private void BtnNextListPage_Clicked(object sender, EventArgs e)
    {
        var popup = new SpinnerPopup();
        this.ShowPopup(popup);
        try
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
            listPatients.ItemsSource = patients;
        }
        finally
        {
            if (patients.Count < 50)
                BtnNextListPage.IsEnabled = false;
            else
                BtnNextListPage.IsEnabled = true;
            popup.Close();
        }
    }
}