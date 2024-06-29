using StareMedic.Models;
using System.Collections.ObjectModel;
using StareMedic.Models.Entities;
using StareMedic.Views.Viewers;
using CommunityToolkit.Maui.Views;
using Microsoft.EntityFrameworkCore.Diagnostics;
using StareMedic.Models.Documents;
using Microsoft.UI.Xaml.Controls.Primitives;
using StareMedic.Data;

namespace StareMedic.Views;

public partial class RegisterClinicalCase : ContentPage
{
    private readonly CasoClinico caso = new(MainRepo.GetCurrentCaseIndex());
    private readonly Diagnostico diag = new(MainRepo.GetCurrentDiagnosticoIndex());
    private Patient patient = new();

    public RegisterClinicalCase()
    {
        InitializeComponent();
        caso.IdDiagnostico = diag.Id;
        EditorDiagnostico.Placeholder = diag.Contenido;
        PickerDoctor.ItemsSource = new ObservableCollection<Medic>(MainRepo.GetMedics());
        PickerHabitacion.ItemsSource = new ObservableCollection<Rooms>(MainRepo.GetRooms());
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
    }

    private void EntryName_TextChanged(object sender, TextChangedEventArgs e)
    {
        caso.Nombre = e.NewTextValue;
    }
 
    private void PickerDoctor_SelectedIndexChanged(object sender, EventArgs e)
    {
        PickerDoctor.Opacity = 0;
        PickerDoctor.FadeTo(1, 200);
        
        if (PickerDoctor.SelectedItem != null)
        caso.IdDoctor = ((Medic)PickerDoctor.SelectedItem).Id;
    }
   
    private async void BtnAddPatient_Clicked(object sender, EventArgs e)
    {
        var popup = new SpinnerPopup();
        this.ShowPopup(popup);
        try
        {
            BtnAddPatient.Opacity = 0;
            await BtnAddPatient.FadeTo(1, 200);

            var addPatientView = new PatientControll(null);
            addPatientView.PatientSelected += OnPatientSelected;

            await Navigation.PushModalAsync(addPatientView);
        }
        finally
        {
            popup.Close();
        }
    }

    private void PickerHabitacion_SelectedIndexChanged(object sender, EventArgs e)
    {
        PickerHabitacion.Opacity = 0;
        PickerHabitacion.FadeTo(1, 400);

        if (PickerDoctor.SelectedItem != null)
            caso.IdHabitacion = ((Rooms)PickerHabitacion.SelectedItem).Id;
    }

    private async void BtnPickPatient_Clicked(object sender, EventArgs e)
    {
        var popup = new SpinnerPopup();
        this.ShowPopup(popup);
        try
        {
            BtnPickPatient.Opacity = 0;
            await BtnPickPatient.FadeTo(1, 200);

            var pickPatientView = new PickPatientView();

            pickPatientView.PatientSelected += OnPatientSelected;
            await Navigation.PushModalAsync(pickPatientView);
        }
        finally
        {
            popup.Close();
        }
    }

    private void OnPatientSelected(object sender, PatientSelectedEventArgs e)
    {

        // Aquí recibo el objeto seleccionado
        var selectedPatient = e.SelectedPatient;

        //asignar eleccion
        patient = selectedPatient;
        caso.IdPaciente = patient.Id;

        //cargar visual
        LblPaciente.Text = patient.Nombre;
        caso.IdPaciente = patient.Id;
        ShowDoctor.IsVisible = true;
        ShowRoom.IsVisible = true;

        // Cierra la página modal
        Navigation.PopModalAsync();
    }

    private async void BtnGuardar_Clicked(object sender, EventArgs e)
    {
        var popup = new SpinnerPopup();
        this.ShowPopup(popup);
        try
        {
            BtnGuardar.Opacity = 0;
            await BtnGuardar.FadeTo(1, 200);
            Thread.Sleep(1000);
            if (caso)
            {
                MakeStringID();
                bool answer = await DisplayAlert("Guardar", $"Se guardara el caso: {caso.Nombre}\nEsta seguro?", "No", "Si");
                if (!answer)
                {
                    Request request = new(1);

                    //guardar diagnostico
                    diag.Contenido = EditorDiagnostico.Text;
                    MainRepo.AddDiagnostico(diag);
                    //caso to DB
                    bool done = false;

                    done = MainRepo.AddCaso(caso);

                    if (done)
                    {
                        var choice = await DisplayAlert("Exito", "¿Desea exportar el caso a PDF?", "No", "Si");
                        if (!choice)
                        {
                            var documento = GenerateAdmisionDoc.GenerateDocument(caso);

                            if (documento)
                            {
                                await DisplayAlert("Exito", $"Se ha guardado el caso con id: {caso.Id}", "Ok");

                            }
                            else
                            {
                                await DisplayAlert("Error", $"No se ha podido exportar el caso:\n{caso.Nombre}", "Ok");
                            }
                            await Shell.Current.GoToAsync("..");
                        }
                        else
                        {
                            await Shell.Current.GoToAsync("..");
                        }
                    }
                    else
                        await DisplayAlert("Error :(", "Hubo un problema guardando el caso clinico en la base, intentalo mas tarde", "Ok");
                }
            }
            else
            {
                await DisplayAlert("Error", $"No se pudo guardar el caso, Datos faltantes:\n{ValidarData()}", "Ok");
            }
        }
        finally
        {
            popup.Close();
        }
    }

    private async void BtnCancel_Clicked(object sender, EventArgs e)
    {
        var popup = new SpinnerPopup();
        this.ShowPopup(popup);
        try
        {
            BtnCancel.Opacity = 0;
            await BtnCancel.FadeTo(1, 200);

            bool answer = await DisplayAlert("Cancelar", "¿Estas seguro de cancelar?", "No", "Si");
            if (!answer)
            {
                await Shell.Current.GoToAsync("..");
            }
        }
        finally
        {
            popup.Close();
        }
    }

    private void MakeStringID()
    {
        string id;
        id = string.Concat("CC", ((Medic)PickerDoctor.SelectedItem).Nombre.AsSpan(0, 1));
        id += LblPaciente.Text[..1];
        id += ((Rooms)PickerHabitacion.SelectedItem).Nombre[..1];
        id += caso.IdDB.ToString();
        caso.Id = id;
    }

    private void DateIngreso_DateSelected(object sender, DateChangedEventArgs e)
    {
        DateIngreso.Opacity = 0;
        DateIngreso.FadeTo(1, 200);

        caso.FechaIngreso = e.NewDate.ToUniversalTime();
    }

    private void RadioMedico_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (RadioMedico.IsChecked)
            caso.TipoCaso = "Medico";
    }

    private void RadioQuirurgico_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (RadioQuirurgico.IsChecked)
            caso.TipoCaso = "Quirurgico";
    }

    private void RadioObstetrico_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (RadioObstetrico.IsChecked)
            caso.TipoCaso = "Obstetrico";
    }

    private void RadioPediatrico_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (RadioPediatrico.IsChecked)
            caso.TipoCaso = "Pediatrico";
    }
    private string ValidarData()
    {
        string Datosfaltantes = "";

        if (string.IsNullOrEmpty(EntryName.Text))
        {
            Datosfaltantes += " Nombre ";
        }

        if (!(RadioMedico.IsChecked || RadioObstetrico.IsChecked || RadioQuirurgico.IsChecked || RadioPediatrico.IsChecked))
        {
            Datosfaltantes += " Tipo De Caso ";
        }
        if (PickerDoctor.SelectedItem == null)
        {
            Datosfaltantes += " Doctor ";
        }
        if (patient.Nombre == null)
        {
            Datosfaltantes += " Paciente ";
        }
        if (PickerHabitacion.SelectedItem == null)
        {
            Datosfaltantes += " Habitacion ";
        }
        return Datosfaltantes;
    }
}