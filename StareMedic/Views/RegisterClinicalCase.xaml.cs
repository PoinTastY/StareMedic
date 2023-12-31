using StareMedic.Models;
using System.Collections.ObjectModel;
using StareMedic.Models.Entities;

namespace StareMedic.Views;

public partial class RegisterClinicalCase : ContentPage
{
    readonly CasoClinico caso = new(MainRepo.GetCurrentCaseIndex());
    public RegisterClinicalCase()
    {
        InitializeComponent();

    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        PickerDoctor.ItemsSource = new ObservableCollection<Medic>(MainRepo.GetMedics());
        if(MainRepo.PatientIdSolver.Nombre != null)
        {
            LblPaciente.Text = MainRepo.PatientIdSolver.Nombre;
            caso.IdPaciente = MainRepo.PatientIdSolver.Id;
            ShowDoctor.IsVisible = true;
            ShowRoom.IsVisible = true;
        }
        PickerHabitacion.ItemsSource = new ObservableCollection<Rooms>(MainRepo.GetRooms());
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        MainRepo.PatientIdSolver = new();
    }

    private void EntryName_TextChanged(object sender, TextChangedEventArgs e)
    {
        caso.Nombre = e.NewTextValue;
    }

    private void PickerDoctor_SelectedIndexChanged(object sender, EventArgs e)
    {
        caso.IdDoctor = ((Medic)PickerDoctor.SelectedItem).Id;
    }

    private void BtnAddPatient_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync($"{nameof(RegisterPatient)}");
    }

    private void PickerHabitacion_SelectedIndexChanged(object sender, EventArgs e)
    {
        caso.IdHabitacion = ((Rooms)PickerHabitacion.SelectedItem).Id;
    }

    private async void BtnPickPatient_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new PickPatientView());
    }

    private async void BtnGuardar_Clicked(object sender, EventArgs e)
    {
        if (caso)
        {
            MakeStringID();
            bool answer = await DisplayAlert("Guardar", $"Se guardara el caso: {caso.Nombre}\nEsta seguro?", "No", "Si");
            if (!answer)
            {
                MainRepo.UpdatePatientStatus(true, caso.IdPaciente);
                MainRepo.UpdateRoomStatus(true, caso.IdHabitacion);
                Diagnostico diag = new(MainRepo.GetCurrentDiagnosticoIndex());
                caso.IdDiagnostico = diag.Id;
                MainRepo.AddDiagnostico(diag);
                MainRepo.AddCaso(caso);
                MainRepo.PatientIdSolver = new();
                await DisplayAlert("Exito", $"Se ha guardado el caso con id: {caso.Id}", "Ok");

                await Shell.Current.GoToAsync("..");
            }
            
        }
        else
        {
            await DisplayAlert("Error", "No se pudo guardar el caso, verifica que todo este bien", "Ok");
        }
    }

    private async void BtnCancel_Clicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Cancelar", "�Estas seguro de cancelar?", "No", "Si");
        if (!answer)
        {
            await Shell.Current.GoToAsync("..");
            MainRepo.PatientIdSolver = new();
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

}