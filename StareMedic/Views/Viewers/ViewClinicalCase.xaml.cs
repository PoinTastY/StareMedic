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

        PickMedic.ItemsSource = MainRepo.GetMedics();
        PickRoom.ItemsSource = MainRepo.GetRooms();

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
        EditorDiagnoose.Text = diagnostico.Contenido;

	}

    

    private void reopenclose(bool status)
    {
        if(status)
        {
            LblStatus.TextColor = Color.FromRgb(0, 255, 0);
            LblStatus.Text = "Estado: Abierto";
            if (medic.Nombre != "missing")
                PickMedic.SelectedItem = medic;
            else
            {
                DisplayAlert("Error", "El registro de medico de este caso no se encontro, elija uno", "Ok");
                enabledisable(true);
            }
            if (room.Nombre != "missing")
                PickRoom.SelectedItem = room;
            else
            {
                DisplayAlert("Error", "El registro de habitacion de este caso no se encontro, elija uno", "Ok");
                enabledisable(true);
            }
            BtnDelete.IsVisible = false;
            BtnDelete.IsEnabled = false;
            BtnSaveAll.IsVisible = false;
            BtnSaveAll.IsEnabled = false;
            BtnCancelAll.IsVisible = false;
            BtnCancelAll.IsEnabled = false;
        }
        else
        {
            LblStatus.TextColor = Color.FromRgb(255, 0, 0);
            LblStatus.Text = "Estado: Cerrado";
            BtnDelete.IsVisible = true;
            BtnDelete.IsEnabled = true;
            enabledisable(false);
            BtnEdit.IsVisible = false;
            BtnEdit.IsEnabled = false;
            BtnSaveAll.IsVisible = false;
            BtnSaveAll.IsEnabled = false;
            BtnCancelAll.IsVisible = false;
            BtnCancelAll.IsEnabled = false;
        }
        BtnCerrarCaso.IsVisible = status;
        BtnCerrarCaso.IsEnabled = status;
        BtnEditDiagnoose.IsVisible = status;
        BtnEditDiagnoose.IsEnabled = status;
        BtnReopenCase.IsVisible = !status;
        BtnReopenCase.IsEnabled = !status;
        BtnEdit.IsVisible = status;
        BtnEdit.IsEnabled = status;

    }

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
        //maybe display a hint os something bcs this emptyness gives me anxiety
        enabledisableDiag(true);
    }

    private void enabledisableDiag (bool status)
    {
        //interaction on buttons of diagnoose
        EditorDiagnoose.IsReadOnly = !status;
        BtnEditDiagnoose.IsVisible = !status;
        BtnEditDiagnoose.IsEnabled = !status;
        BtnSaveDiagnoose.IsEnabled = status;
        BtnSaveDiagnoose.IsVisible = status;
        BtnCancelEditionDiagnoose.IsEnabled = status;
        BtnCancelEditionDiagnoose.IsVisible = status;
    }

    private async void BtnSaveDiagnoose_Clicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Confirmar", $"Se guardaran los cambios en el diagnostico:\n{caso.Id}", "Cancelar", "Confirmar");
        if (!confirm)
        {
            //update changefs
            diagnostico.Contenido = EditorDiagnoose.Text;
            MainRepo.UpdateDiagnoose(diagnostico);
            await DisplayAlert("Confirmado", $"Se han guardado los cambios en el diagnostico:\n{caso.Id}", "Ok");
            enabledisableDiag(false);

        }
        else
        {
            //get back
            await DisplayAlert("Cancelado", $"NO se han guardado los cambios en el diagnostico:\n{caso.Id}", "Ok");
            EditorDiagnoose.Text = diagnostico.Contenido;
            enabledisableDiag(false);
            return;
        }
		
    }

    private async void BtnCancelEditionDiagnoose_Clicked(object sender, EventArgs e)
    {
        bool done = await DisplayAlert("Confirmar", $"Deseas cancelar la edicion del diagnostico?", "No", "Si");
        if (!done)
        {
            //confirm anything that happens so the user dont too silly things
            await DisplayAlert("Cancelado", $"NO se han guardado los cambios en el diagnostico", "Ok");
            EditorDiagnoose.Text = diagnostico.Contenido;
            enabledisableDiag(false);
            return;
        }
        else
        {
            return;
        }
    }



    private async void BtnReopenCase_Clicked(object sender, EventArgs e)
    {
        //first confirm if we want to reopen
        bool confirm = await DisplayAlert("Confirmar:", $"Se reabrir� el caso:\n{caso.Id}", "Cancelar", "Confirmar");
        if(!confirm)//remember, is inverted
        {
            if (patient.Status == true)//if patient is busy, we cant reopen
            {
                await DisplayAlert("Error:", $"El paciente:\n{patient.Nombre}\nEsta ocupado", "Ok");
                return;
            }
            else//if not, we reopen
            {

                MainRepo.ReopenCase(caso.IdDB);
                await DisplayAlert("Confirmado:", $"Se ha reabierto el caso:\n{caso.Nombre}", "Ok");
                reopenclose(true);//then reload butons and that
            }
        }
    }

    private async void BtnSaveAll_Clicked(object sender, EventArgs e)
    { 
        if(PickMedic.SelectedItem == null || PickRoom.SelectedItem == null || string.IsNullOrWhiteSpace(EntryName.Text))
        {
            await DisplayAlert("Error:", "No se puede guardar, verifica que todo este bien", "Ok");
            return;
        }
        bool choice = await DisplayAlert("Confirmar:", $"Se guardaran los cambios en el caso:\n{caso.Id}", "Cancelar", "Confirmar");
        if (!choice)
        {
            //update changefs
            medic = (Medic)PickMedic.SelectedItem;
            room = (Rooms)PickRoom.SelectedItem;
            caso.IdDoctor = medic.Id;
            caso.IdHabitacion = room.Id;
            caso.Nombre = EntryName.Text;
            MainRepo.UpdateClinicalCase(caso);
            await DisplayAlert("Confirmado", $"Se han guardado los cambios en el caso:\n{caso.Nombre}", "Ok");
            enabledisable(false);

        }
        else
        {
            await DisplayAlert("Cancelado", $"NO se han guardado los cambios en el caso:\n{caso.Nombre}", "Ok");
            PickMedic.SelectedItem = medic;
            PickRoom.SelectedItem = room;
            EntryName.Text = caso.Nombre;
            enabledisable(false);
            return;
        }
        
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

    private async void BtnCancelAll_Clicked(object sender, EventArgs e)
    {
        if(medic.Nombre == "missing" || room.Nombre == "missing")
        {
            await DisplayAlert("Advertencia:", "Si el medico o habitaciones asignadas no son validos,\nSe recomienda elegir a uno", "Ok");
        }

        bool choice = await DisplayAlert("Deshacer", $"Deseas cancelar los cambios?\nEl caso volvera al estado anterior", "No", "Si");
        if (!choice)
        {
            await DisplayAlert("Cancelado", $"NO se han guardado los cambios en el caso:\n{caso.Nombre}", "Ok");
            EntryName.Text = caso.Nombre;
            PickMedic.SelectedItem = medic;
            PickRoom.SelectedItem = room;
            enabledisable(false);
            return;
        }
        else
        {
            return;
        }

    }

    private async void BtnDelete_Clicked(object sender, EventArgs e)
    {
        //WE CANT DELETE AN OPEN CASE, FIRST WE NEED TO CLOSE IT, SO VALIDATE THAT
        if(caso.Activo == true)
        {
            await DisplayAlert("Error", $"No se puede eliminar un caso abierto", "Ok");
            return;
        }else
        {
            bool confirm = await DisplayAlert("Confirmar", $"Se eliminara el caso:\n{caso.Id}", "Cancelar", "Confirmar");
            if (!confirm)
            {

                MainRepo.DeleteClinicalCase(caso);
                await DisplayAlert("Confirmado", $"Se ha eliminado el caso:\n{caso.Nombre}", "Ok");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Cancelado", $"NO se ha eliminado el caso", "Ok");
                return;
            }
        }   
    }
}