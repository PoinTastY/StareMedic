using StareMedic.Models;
using System.Collections.ObjectModel;
using StareMedic.Models.Entities;
using StareMedic.Views.Viewers;
using CommunityToolkit.Maui.Views;
using Microsoft.EntityFrameworkCore.Diagnostics;

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

        if(PickerDoctor.SelectedItem != null)
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

            await Navigation.PushModalAsync(new PatientControll(null));
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
                        //push to SDK
                        try
                        {
                            double x = 0;
                            x = request.FillPackAndPush(caso, caso.Paciente(), caso.Habitacion(), caso.Medico(), caso.Diagnostico());

                            if (x > 0)
                            {
                                await DisplayAlert("Exito!", $"Se ha generado la remision de la admision\nFolio: {x}", "Ok");
                                caso.FolioSDK = x;
                            }
                            else
                            {
                                await DisplayAlert("Error", $"No se ha generado la remision en Contpaqi, intenta mas tarde\nRespuesta del servidor: {x}", "OK");
                            }
                        }
                        catch (Exception ex)
                        {
                            await DisplayAlert("Error SDK", $"Hubo un problema generando el documento en Contpaqi: {ex}", "Enterado");
                        }


                        bool documento = false;
                        documento = DoCreate.GenerateDocument(caso);

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
                        await DisplayAlert("Error :(", "Hubo un problema guardando el caso clinico en la base, intentalo mas tarde", "Ok");
                }
            }
            else
            {
                await DisplayAlert("Error", "No se pudo guardar el caso, verifica que todo este bien", "Ok");
            }
        }
        finally
        {
            popup.Close();
        }
    }

    private async void BtnCancel_Clicked(object sender, EventArgs e)
    {
        BtnCancel.Opacity = 0;
        await BtnCancel.FadeTo(1, 200);

        bool answer = await DisplayAlert("Cancelar", "¿Estas seguro de cancelar?", "No", "Si");
        if (!answer)
        {
            var popup = new SpinnerPopup();
            this.ShowPopup(popup);
            try
            {
                await Shell.Current.GoToAsync("..");
            }
            finally
            {
                popup.Close();
            }
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
        if(RadioMedico.IsChecked)
            caso.TipoCaso = "Medico";
    }

    private void RadioQuirurgico_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if(RadioQuirurgico.IsChecked)
            caso.TipoCaso = "Quirurgico";
    }

    private void RadioObstetrico_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if(RadioObstetrico.IsChecked)
            caso.TipoCaso = "Obstetrico";
    }
}