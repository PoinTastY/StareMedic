using CommunityToolkit.Maui.Views;
using StareMedic.Models;
using StareMedic.Models.Documents;
using StareMedic.Models.Entities;
using StareMedic.Views.Viewers;
using System.Reflection.Metadata;

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

        if (medic.Nombre == "missing" || room.Nombre == "missing")
            BtnSendSDK.IsEnabled = false;


        if (caso.FolioSDK != 0 && caso.FolioSDK != null)
        {
            LblFolioSDK.Text = $"Folio Contpaqi: {caso.FolioSDK}";
            LblFolioSDK.TextColor = Color.FromRgb(0,161,53);
            BtnSendSDK.IsEnabled = false;
            BtnSendSDK.IsVisible = false;
        }

        PickMedic.ItemsSource = MainRepo.GetMedics();
        PickRoom.ItemsSource = MainRepo.GetRooms();

        if (medic.Nombre != "missing")
        {
            PickMedic.SelectedItem = medic;
        }
        if (room.Nombre != "missing")
        {
            PickRoom.SelectedItem = room;
        }

        if (medic.Nombre == "missing" || room.Nombre == "missing")
        {
            DisplayAlert("Advertencia:", "Si el medico o habitaciones asignadas no son validos,\nSe recomienda elegir a uno", "Ok");
        }

        //Fill everything
        LblName.Text = caso.Nombre;
		LblId.Text = "ID: " + caso.Id;
		EntryName.Text = caso.Nombre;
		EntryPatient.Text = patient.Nombre;
        EditorDiagnoose.Text = diagnostico.Contenido;

        //fecha ingreso
        DateIngreso.Date = caso.FechaIngreso.LocalDateTime;
        
        //tipo de caso
        if(caso.TipoCaso == "Medico")
            RadioMedico.IsChecked = true;
        if(caso.TipoCaso == "Obstetrico")
            RadioObstetrico.IsChecked = true;
        if(caso.TipoCaso == "Quirurgico")
            RadioQuirurgico.IsChecked = true;
        if (caso.TipoCaso == "Pediatrico")
            RadioPediatrico.IsChecked = true;

	}

    private async void BtnEditDiagnoose_Clicked(object sender, EventArgs e)
    {
        BtnEditDiagnoose.Opacity = 0;

        await BtnEditDiagnoose.FadeTo(1, 300);
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
        BtnCancel2.IsEnabled = !status;
        BtnCancel2.IsVisible = !status;
        BtnExportCase.IsEnabled = !status;
        BtnExportCase.IsVisible = !status;
        BtnEdit.IsEnabled = !status;
        BtnEdit.IsVisible = !status;
        BtnDelete.IsEnabled = !status;
        BtnDelete.IsVisible = !status;

    }

    private async void BtnSaveDiagnoose_Clicked(object sender, EventArgs e)
    {
        var popup = new SpinnerPopup();
        this.ShowPopup(popup);
        try
        {
            BtnSaveDiagnoose.Opacity = 0;

            await BtnSaveDiagnoose.FadeTo(1, 300);
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
        finally
        {
            popup.Close();
        }
    }

    private async void BtnCancelEditionDiagnoose_Clicked(object sender, EventArgs e)
    {
        BtnCancelEditionDiagnoose.Opacity = 0;

        await BtnCancelEditionDiagnoose.FadeTo(1, 300);
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

    private async void BtnSaveAll_Clicked(object sender, EventArgs e)
    {
        var popup = new SpinnerPopup();
        this.ShowPopup(popup);
        try
        {
            BtnSaveAll.Opacity = 0;

            await BtnSaveAll.FadeTo(1, 300);
            if (PickMedic.SelectedItem == null || PickRoom.SelectedItem == null || string.IsNullOrWhiteSpace(EntryName.Text))
            {
                await DisplayAlert("Error:", "No se puede guardar, verifica que todo este bien", "Ok");
                return;
            }
            bool choice = await DisplayAlert("Confirmar:", $"Se guardaran los cambios en el caso:\n{caso.Id}", "Cancelar", "Confirmar");
            if (!choice)
            {
                //update changefs
                if (RadioMedico.IsChecked)
                    caso.TipoCaso = "Medico";
                if (RadioQuirurgico.IsChecked)
                    caso.TipoCaso = "Quirurgico";
                if (RadioObstetrico.IsChecked)
                    caso.TipoCaso = "Obstetrico";
                caso.FechaIngreso = DateIngreso.Date.ToUniversalTime();
                medic = (Medic)PickMedic.SelectedItem;
                room = (Rooms)PickRoom.SelectedItem;
                caso.IdDoctor = medic.Id;
                caso.IdHabitacion = room.Id;
                caso.Nombre = EntryName.Text;
                MainRepo.UpdateClinicalCase(caso);
                await DisplayAlert("Confirmado", $"Se han guardado los cambios en el caso:\n{caso.Nombre}", "Ok");
                enabledisable(false);
                if (medic.Nombre != "missing" && room.Nombre != "missing")
                    BtnSendSDK.IsEnabled = true;

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
        finally
        {
            popup.Close();
        }
    }

    private async void BtnEdit_Clicked(object sender, EventArgs e)
    {
        BtnEdit.Opacity = 0;

        await BtnEdit.FadeTo(1, 300);
        if (medic.Nombre == "missing" || room.Nombre == "missing")
        {
            await DisplayAlert("Advertencia:", "Si el medico o habitaciones asignadas no son validos,\nSe recomienda elegir a uno", "Ok");
            BtnSendSDK.IsEnabled = false;
        }
        enabledisable(true);

    }



    //btn to abort changes and get back to the previous state
    private async void BtnCancelAll_Clicked(object sender, EventArgs e)
    {
        BtnCancelAll.Opacity = 0;

        await BtnCancelAll.FadeTo(1, 300);
        if(medic.Nombre == "missing" || room.Nombre == "missing")
        {
            await DisplayAlert("Advertencia:", "Si el medico o habitaciones asignadas no son validos,\nSe recomienda elegir a uno", "Ok");
            BtnSendSDK.IsEnabled = false;
        }

        bool choice = await DisplayAlert("Deshacer", $"Deseas cancelar los cambios?\nEl caso volvera al estado anterior", "No", "Si");
        if (!choice)
        {
            await DisplayAlert("Cancelado", $"NO se han guardado los cambios en el caso:\n{caso.Nombre}", "Ok");
            EntryName.Text = caso.Nombre;
            if (medic.Nombre != "missing")
                PickMedic.SelectedItem = medic;
            if (room.Nombre != "missing")
                PickRoom.SelectedItem = room;
            enabledisable(false);
            return;
        }
        else
        {
            return;
        }
    }

    private async void BtnCancel2_Clicked(object sender, EventArgs e)
    {
        var popup = new SpinnerPopup();
        this.ShowPopup(popup);
        try
        {
            BtnCancel2.Opacity = 0;

            await BtnCancel2.FadeTo(1, 300);
            await Shell.Current.GoToAsync("..");
        }
        finally
        {
            popup.Close();
        }
    }

    //Btn to delete the case
    private async void BtnDelete_Clicked(object sender, EventArgs e)
    {
        var popup = new SpinnerPopup();
        this.ShowPopup(popup);
        try
        {
            BtnDelete.Opacity = 0;

            await BtnDelete.FadeTo(1, 300);
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
        finally
        {
            popup.Close();
        }
    }

    //Btn to call DoCreate and export the case
    private async void BtnExportCase_Clicked(object sender, EventArgs e)
    {
        var popup = new SpinnerPopup();
        this.ShowPopup(popup);
        try
        {
            var Confirm = await DisplayAlert("Confirmar", $"Se exportara el caso:\n{caso.Id}", "Cancelar", "Confirmar");

            BtnExportCase.Opacity = 0;
            await BtnExportCase.FadeTo(1, 300);
            if (!Confirm)
            {
                var documento = GenerateAdmisionDoc.GenerateDocument(caso);
                if (documento)
                {
                    await DisplayAlert("Confirmado", $"Se ha exportado el caso:\n{caso.Nombre}", "Ok");
                }
                else
                {
                    await DisplayAlert("Error", $"No se ha podido exportar el caso:\n{documento}", "Ok");
                }//TODO: EXCEPTION MANAGEMENT
            }
        }
        finally
        {
            popup.Close();
        }
    }

    //View elements visibility and interaction
    private void enabledisable(bool status)
    {
        
        //interaction of edit buttons
        BtnEdit.IsEnabled = !status;
        BtnEdit.IsVisible = !status;
        BtnDelete.IsEnabled = !status;
        BtnDelete.IsVisible = !status;
        BtnSaveAll.IsVisible = status;
        BtnSaveAll.IsEnabled = status;
        BtnCancelAll.IsVisible = status;
        BtnCancelAll.IsEnabled = status;
        BtnCancel2.IsEnabled = !status;
        BtnCancel2.IsVisible = !status;
        BtnExportCase.IsEnabled = !status;
        BtnExportCase.IsVisible = !status;

        BtnEditDiagnoose.IsVisible = !status;
        BtnEditDiagnoose.IsEnabled = !status;


        //interaction on data
        EntryName.IsEnabled = status;
        PickMedic.IsEnabled = status;
        PickRoom.IsEnabled = status;

        //date and radio buttons
        DateIngreso.IsEnabled = status;
        RadioMedico.IsEnabled = status;
        RadioQuirurgico.IsEnabled = status;
        RadioObstetrico.IsEnabled = status;
        RadioPediatrico.IsEnabled = status;
    }

    private async void BtnSendSDK_Clicked(object sender, EventArgs e)
    {
        var popup = new SpinnerPopup();
        this.ShowPopup(popup);
        try
        {
            BtnSendSDK.Opacity = 0;

            await BtnSendSDK.FadeTo(1, 300);
            Request req = new(1);
            try
            {
                var confirm = await DisplayAlert("Confirmacion", "Quieres enviar la remision a Contpaqi?\nRecuerda validar todos los datos", "No", "Si");
                if (!confirm)
                {
                    double x = req.FillPackAndPush(caso, caso.Paciente(), caso.Habitacion(), caso.Medico(), caso.Diagnostico());
                    if (x > 0)
                    {
                        await DisplayAlert("Exito!", $"Se ha generado la remision de la admision\nFolio: {x}", "Ok");
                        caso.FolioSDK = x;
                        LblFolioSDK.Text = $"Folio Contpaqi: {caso.FolioSDK}";
                        LblFolioSDK.TextColor = Color.FromRgb(0, 161, 53);
                        BtnSendSDK.IsEnabled = false;
                        BtnSendSDK.IsVisible = false;
                    }
                    else
                    {
                        await DisplayAlert("Error", $"No se ha generado la remision en Contpaqi, intenta mas tarde\nRespuesta del servidor: {x}", "OK");
                        caso.FolioSDK = x;
                    }
                    MainRepo.UpdateClinicalCase(caso);
                }
            }
            catch
            {
                await DisplayAlert("Error SDK", $"Hubo un problema generando el documento en Contpaqi\nContacta al administrador para verificar el servicio del SDK en el servidor", "Enterado");
            }
        }
        finally
        {
            popup.Close();
        }
    }
}