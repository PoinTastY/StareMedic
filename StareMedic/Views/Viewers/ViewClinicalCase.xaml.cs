using StareMedic.Models;
using StareMedic.Models.Entities;
using System.Collections.ObjectModel;

namespace StareMedic.Views;

public partial class ViewClinicalCase : ContentPage
{
	private readonly CasoClinico caso;
	private readonly Patient patient;
	private readonly Medic medic;
	private readonly Rooms room;
	private readonly Diagnostico diagnostico;

    private Picker PickerRoom;
    private Picker PickerMedic;
    

    public ViewClinicalCase(CasoClinico caso)
	{
		InitializeComponent();
		//objects init
		this.caso = caso;
        patient = caso.Paciente();
		medic = caso.Medico();
		room = caso.Habitacion();
		diagnostico = caso.Diagnostico();




        //Fill everything
        LblName.Text = caso.Nombre;
		LblId.Text = "ID: " + caso.Id;
		EntryName.Text = caso.Nombre;
		EntryPatient.Text = patient.Nombre;
		EntryMedic.Text = medic.Nombre;
		EntryRoom.Text = room.Nombre;
		EditorDiagnoose.Text = diagnostico.Contenido;

        //self explanatory
        if(this.caso.Activo != true)
        {
            BtnReopenCase.IsVisible = true;
        }
        else
        {
            BtnReopenCase.IsVisible = false;
        }

	}

	

	//TODO: Implement diagnoose, i think it is the only thing actually needed
    private async void BtnCerrarCaso_Clicked(object sender, EventArgs e)
    {
		bool confirm = await DisplayAlert("Confirmar", $"Se cerrara el caso:\n{caso.Id}", "Cancelar", "Confirmar"); 
		if(!confirm)
		{
			MainRepo.CloseCase(caso.IdDB);
			await DisplayAlert("Confirmado", $"Se ha cerrado el caso:\n{caso.Nombre}", "Ok");
			await Navigation.PopAsync();
		}
    }

    private async void BtnExportCase_Clicked(object sender, EventArgs e)
    {
		var Confirm = await DisplayAlert("Confirmar", $"Se exportara el caso:\n{caso.Id}", "Cancelar", "Confirmar");
		if (!Confirm)
		{
			if (DoCreate.GenerateDocument(caso)) { 
				await DisplayAlert("Confirmado", $"Se ha exportado el caso:\n{caso.Nombre}", "Ok");
			}
			else
			{
				await DisplayAlert("Error", $"No se ha podido exportar el caso:\n{caso.Nombre}", "Ok");
			}//TODO: EXCEPTION MANAGEMENT
			
		}
    }

    private void BtnEditDiagnoose_Clicked(object sender, EventArgs e)
    {
		EditorDiagnoose.IsReadOnly = false;
		BtnEditDiagnoose.IsVisible = false;
		BtnEditDiagnoose.IsEnabled = false;
		//BtnSaveDiagnoose.IsVisible = true; or create it directly, iunno
		BtnSaveDiagnoose.IsEnabled = true;
		BtnSaveDiagnoose.IsVisible = true;
		BtnCancelEditionDiagnoose.IsEnabled = true;
		BtnCancelEditionDiagnoose.IsVisible = true;

    }

    private void BtnSaveDiagnoose_Clicked(object sender, EventArgs e)
    {
		diagnostico.Contenido = EditorDiagnoose.Text;
        MainRepo.UpdateDiagnoose(diagnostico);
        EditorDiagnoose.IsReadOnly = true;
        BtnEditDiagnoose.IsVisible = true;
        BtnEditDiagnoose.IsEnabled = true;
        BtnSaveDiagnoose.IsEnabled = false;
        BtnSaveDiagnoose.IsVisible = false;
		BtnCancelEditionDiagnoose.IsEnabled = false;
		BtnCancelEditionDiagnoose.IsVisible = false;
    }

    private void BtnCancelEditionDiagnoose_Clicked(object sender, EventArgs e)
    {
		EditorDiagnoose.Text = diagnostico.Contenido;
        EditorDiagnoose.IsReadOnly = true;
        BtnEditDiagnoose.IsVisible = true;
        BtnEditDiagnoose.IsEnabled = true;
        BtnSaveDiagnoose.IsEnabled = false;
        BtnSaveDiagnoose.IsVisible = false;
        BtnCancelEditionDiagnoose.IsEnabled = false;
        BtnCancelEditionDiagnoose.IsVisible = false;
    }

    private void BtnChangeCaseName_Clicked(object sender, EventArgs e)
    {

    }

    private void BtnChangeMedic_Clicked(object sender, EventArgs e)
    {
		ObservableCollection<Medic> medics = new();
		foreach (Medic medic in MainRepo.GetMedics())
		{
            medics.Add(medic);
        }
		Editables.Children.Remove(EntryMedic);
        PickerMedic= new Picker()
        {
            ItemsSource = medics
        };
        PickerMedic.ItemDisplayBinding = new Binding("Nombre");
        PickerMedic.SelectedIndexChanged += PickerMedic_SelectedIndexChanged;
        Editables.Children.Insert(1,PickerMedic);
    }

    private async void BtnChangeRoom_Clicked(object sender, EventArgs e)
    {
		bool sure = await DisplayAlert("Confirmar", "Se cambiara la habitacion del caso", "Cancelar", "Confirmar");
		if (!sure)
		{
            ObservableCollection<Rooms> rooms = new();
            foreach (Rooms room in MainRepo.GetRooms())
            {
                rooms.Add(room);
            }
            Editables.Children.Remove(EntryRoom);
            PickerRoom = new Picker
            {
                ItemsSource = rooms,
            };
            PickerRoom.ItemDisplayBinding = new Binding("Nombre");
            PickerRoom.SelectedIndexChanged += PickerRoom_SelectedIndexChanged;
            Editables.Children.Insert(4, PickerRoom);

        }
        
    }

    private void PickerRoom_SelectedIndexChanged(object sender, EventArgs e)
	{
        if(PickerRoom != null)
        {
            caso.IdHabitacion = ((Rooms)PickerRoom.SelectedItem).Id;
        }
        BtnSaveAll.IsEnabled = true;
    }

    private void PickerMedic_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(PickerMedic != null)
        {
            caso.IdDoctor = ((Medic)PickerMedic.SelectedItem).Id;
        }
        BtnSaveAll.IsEnabled = true;
    }



    private void BtnReopenCase_Clicked(object sender, EventArgs e)
    {
        //first confirm if we want to reopen

        //then disable btn
        BtnReopenCase.IsVisible = false;
        caso.Activo = true;
        //then, re enable edits
        //remember to update new close 
        //validate if patient is not busy to reopen
        if(MainRepo.GetPatientById(patient.Id).Status != true)
        {
            MainRepo.ReopenCase(caso.IdDB);
        }
        //make sure that all affected entities are good w dis
    }

    private void BtnSaveAll_Clicked(object sender, EventArgs e)
    {
        MainRepo.UpdateClinicalCase(caso);
    }
}