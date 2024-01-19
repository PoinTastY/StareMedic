using StareMedic.Models;
using StareMedic.Models.Entities;

namespace StareMedic.Views;

public partial class ViewClinicalCase : ContentPage
{
	private readonly CasoClinico caso;
	private readonly Patient patient;
	private readonly Diagnostico diagnostico;
    private Medic medic;
    private Rooms room;
    

    public ViewClinicalCase(CasoClinico caso)
	{
		InitializeComponent();
		//objects init
		this.caso = caso;
        patient = caso.Paciente();
		medic = caso.Medico();
		room = caso.Habitacion();
		diagnostico = caso.Diagnostico();



        if (caso.Activo == true)
        {
            reopenclose(true);
        }
        else
        {
            reopenclose(false);
        }
        //Fill everything
        LblName.Text = caso.Nombre;
		LblId.Text = "ID: " + caso.Id;
		EntryName.Text = caso.Nombre;
		EntryPatient.Text = patient.Nombre;
		PickMedic.ItemsSource = MainRepo.GetMedics();
        PickMedic.SelectedItem = medic;
        PickRoom.ItemsSource = MainRepo.GetRooms();
        PickRoom.SelectedItem = room;
		EditorDiagnoose.Text = diagnostico.Contenido;

	}

    private void reopenclose(bool status)
    {
        if(status)
        {
            LblStatus.TextColor = Color.FromRgb(0, 255, 0);
            LblStatus.Text = "Estado: Abierto";
        }
        else
        {
            LblStatus.TextColor = Color.FromRgb(255, 0, 0);
            LblStatus.Text = "Estado: Cerrado";
        }
        BtnCerrarCaso.IsVisible = status;
        BtnCerrarCaso.IsEnabled = status;
        BtnEditDiagnoose.IsVisible = status;
        BtnEditDiagnoose.IsEnabled = status;
        BtnReopenCase.IsVisible = !status;
        BtnReopenCase.IsEnabled = !status;
        BtnEdit.IsVisible = status;
    }

	

	//TODO: Implement diagnoose, i think it is the only thing actually needed
    private async void BtnCerrarCaso_Clicked(object sender, EventArgs e)
    {
		bool confirm = await DisplayAlert("Confirmar", $"Se cerrara el caso:\n{caso.Id}", "Cancelar", "Confirmar"); 
		if(!confirm)
		{
			MainRepo.CloseCase(caso.IdDB);
			await DisplayAlert("Confirmado", $"Se ha cerrado el caso:\n{caso.Nombre}", "Ok");
            reopenclose(false);
        }
        else
        {
            return;
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
        enabledisableDiag(true);

    }

    private void enabledisableDiag (bool status)
    {
        EditorDiagnoose.IsReadOnly = !status;
        BtnEditDiagnoose.IsVisible = !status;
        BtnEditDiagnoose.IsEnabled = !status;
        BtnSaveDiagnoose.IsEnabled = status;
        BtnSaveDiagnoose.IsVisible = status;
        BtnCancelEditionDiagnoose.IsEnabled = status;
        BtnCancelEditionDiagnoose.IsVisible = status;
    }

    private void BtnSaveDiagnoose_Clicked(object sender, EventArgs e)
    {
		diagnostico.Contenido = EditorDiagnoose.Text;
        MainRepo.UpdateDiagnoose(diagnostico);
        enabledisableDiag(false);
    }

    private void BtnCancelEditionDiagnoose_Clicked(object sender, EventArgs e)
    {
		enabledisableDiag(false);
    }



    private async void BtnReopenCase_Clicked(object sender, EventArgs e)
    {
        //first confirm if we want to reopen
        bool confirm = await DisplayAlert("Confirmar", $"Se reabrir� el caso:\n{caso.Id}", "Cancelar", "Confirmar");
        if(confirm)//remember, is inverted
        {
            return;
        }
        //then disable btn
        reopenclose(true);
        caso.Activo = true;
        MainRepo.ReopenCase(caso.IdDB);

        //then, re enable edits
        //remember to update new close 
        //validate if patient is not busy to reopen
        
        //make sure that all affected entities are good w dis
    }

    private void BtnSaveAll_Clicked(object sender, EventArgs e)
    { 
        medic = (Medic)PickMedic.SelectedItem;
        room = (Rooms)PickRoom.SelectedItem;
        caso.IdDoctor = medic.Id;
        caso.IdHabitacion = room.Id;
        caso.Nombre = EntryName.Text;
        enabledisable(false);
        MainRepo.UpdateClinicalCase(caso);
    }

    private void BtnEdit_Clicked(object sender, EventArgs e)
    {

        enabledisable(true);

    }

    private void enabledisable(bool status)
    {
        BtnEdit.IsEnabled = !status;
        BtnSaveAll.IsVisible = status;
        BtnSaveAll.IsEnabled = status;
        BtnCancelAll.IsVisible = status;
        BtnCancelAll.IsEnabled = status;
        EntryName.IsEnabled = status;
        PickMedic.IsEnabled = status;
        PickRoom.IsEnabled = status;
    }

    private void BtnCancelAll_Clicked(object sender, EventArgs e)
    {
        EntryName.Text = caso.Nombre;
        PickMedic.SelectedItem = medic;
        PickRoom.SelectedItem = room;
        enabledisable(false);
    }
}