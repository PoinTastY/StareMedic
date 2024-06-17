using StareMedic.Models.Entities;
using StareMedic.Models;

namespace StareMedic.Views.Viewers;

public partial class PatientControll : ContentPage
{
    private readonly Patient paciente;
    private Cercano cercano;
    private Fiador fiador;

    public PatientControll(Patient pacientEdit)
	{
		InitializeComponent();

        if (pacientEdit != null)
        {
            paciente = pacientEdit;
            enableEdit(false);
            FillFields();
        }
        else
        {
            paciente = new(MainRepo.GetCurrentPatientIndex());
            cercano = new(MainRepo.GetCurrentCercanoIndex());
            fiador = new(MainRepo.GetCurrentFiadorIndex());
        }

        lblID.Text = "ID: " + paciente.Id.ToString();
	}

    //Patient
    private void EntryName_TextChanged(object sender, TextChangedEventArgs e)
    {
        paciente.Nombre = entryName.Text;
    }

    private void EntryTelefono_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(e.NewTextValue) && !ValidationHelper.IsNumeric(e.NewTextValue))
        {
            entryTelefono.Text = e.OldTextValue;
        }
        else
        {
            paciente.Telefono = entryTelefono.Text;
        }
    }

    private void PickerSexo_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (pickerSexo.SelectedIndex == 0)
        {
            paciente.Sexo = 'M';
        }
        else if (pickerSexo.SelectedIndex == 1)
        {
            paciente.Sexo = 'F';
        }
        else
        {
            paciente.Sexo = 'O';
        }

    }

    private void PickerEdoCivil_SelectedIndexChanged(object sender, EventArgs e)
    {
        paciente.EstadoCivil = (string)pickerEdoCivil.SelectedItem;

    }


    private void
        EntryDomicilio_TextChanged(object sender, TextChangedEventArgs e)
    {
        paciente.Domicilio = entryDomicilio.Text;
    }

    private void EntryNacionalidad_TextChanged(object sender, TextChangedEventArgs e)
    {
        paciente.Nacionalidad = entryNacionalidad.Text;
    }

    private void EntryEstado_TextChanged(object sender, TextChangedEventArgs e)
    {
        paciente.Estado = entryEstado.Text;
    }

    private void EntryCiudad_TextChanged(object sender, TextChangedEventArgs e)
    {
        paciente.Ciudad = entryCiudad.Text;
    }

    //Cercano
    private void EntryNombreCercano_TextChanged(object sender, TextChangedEventArgs e)
    {
        cercano.Nombre = entryNombreCercano.Text;
    }

    private void EntryTelefonoCercano_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(e.NewTextValue) && !ValidationHelper.IsNumeric(e.NewTextValue))
        {
            entryTelefonoCercano.Text = e.OldTextValue;
        }
        else
        {
            cercano.Telefono = entryTelefonoCercano.Text;
        }

    }

    private void EntryDomicilioCercano_TextChanged(object sender, TextChangedEventArgs e)
    {
        cercano.Direccion = entryDomicilioCercano.Text;
    }

    private void EntryCiudadCercano_TextChanged(object sender, TextChangedEventArgs e)
    {
        cercano.Ciudad = entryCiudadCercano.Text;
    }

    private void EntryEstadoCercano_TextChanged(object sender, TextChangedEventArgs e)
    {
        cercano.Estado = entryEstadoCercano.Text;
    }

    private void EntryRelacionCercano_TextChanged(object sender, TextChangedEventArgs e)
    {
        cercano.Relacion = entryRelacionCercano.Text;
    }

    //confirm buttons
    private async void BtnCancel_Clicked(object sender, EventArgs e)
    {
        BtnCancel.Opacity = 0;

        await BtnCancel.FadeTo(1, 300);
        bool confirm = await DisplayAlert("Cancelar", "Desea cancelar el registro?", "No", "Si");
        if (!confirm)
        {
            await DisplayAlert("Cancelado", "No se ha realizado ningun cambio", "Ok");
            await Shell.Current.GoToAsync("..");
        }
    }

    private async void BtnGuardar_Clicked(object sender, EventArgs e)
    {
        BtnGuardar.Opacity = 0;

        await BtnGuardar.FadeTo(1, 300);

        if (paciente)
        {
            //the cancel and confirm buttons are switched bcs i like it that way
            bool confirmation = await DisplayAlert("Confirmar", $"Se registrara a:\n{paciente.Nombre}", "Cancelar", "Confirmar");
            if (!confirmation)//that is actually true lol
            {
                //add 2 repo
                if (cercano)
                {

                    paciente.IdCercano = cercano.Id;
                    MainRepo.AddCercano(cercano);
                    fiador.Copy(cercano);
                    MainRepo.AddFiador(fiador);
                    paciente.IdFiador = fiador.Id;

                    //simplified capture to force cercano data capture and use it always as fiador
                }
                else
                {
                    await DisplayAlert("Error","No puede haber Campos vacios en la informacion de Cercano", "Ok");
                    return;
                }

                if (MainRepo.AddPatient(paciente))
                    await DisplayAlert("Exito", $"Paciente registrado: {paciente.Nombre}", "OK");
                else
                    await DisplayAlert("Error", "Hubo un problema registrando al paciente, intentalo mas tarde.", "Ok");
                //go back
                await Shell.Current.GoToAsync("..");
            }
        }
        else
        {
            await DisplayAlert("Error:", "No se puede registrar un paciente sin nombre", "OK");
            return;
        }
    }

    private void FillFields()
    {
        //fill the fields
        entryName.Text = paciente.Nombre;
        entryTelefono.Text = paciente.Telefono;
        entryDomicilio.Text = paciente.Domicilio;
        entryNacionalidad.Text = paciente.Nacionalidad;
        entryEstado.Text = paciente.Estado;
        entryCiudad.Text = paciente.Ciudad;
        EntryEdad.Text = paciente.Edad;
        pickerEdoCivil.SelectedItem = paciente.EstadoCivil;

        if (paciente.Sexo == 'M')
        {
            pickerSexo.SelectedItem = "Masculino";
        }
        else if (paciente.Sexo == 'F')
        {
            pickerSexo.SelectedItem = "Femenino";
        }
        else
        {
            pickerSexo.SelectedItem = "Otro";
        }
        
        //cercano
        if (paciente.IdCercano != null)
        {
            cercano = new (MainRepo.GetCercanoById(paciente.IdCercano));
            entryNombreCercano.Text = cercano.Nombre;
            entryTelefonoCercano.Text = cercano.Telefono;
            entryDomicilioCercano.Text = cercano.Direccion;
            entryCiudadCercano.Text = cercano.Ciudad;
            entryEstadoCercano.Text = cercano.Estado;
            entryRelacionCercano.Text = cercano.Relacion;
        }
        else
        {
            cercano = new(MainRepo.GetCurrentCercanoIndex());
        }

        if (paciente.IdFiador != null)
        {
            fiador = MainRepo.GetFiadorById(paciente.IdFiador);
        }
        else
        {
            fiador = new(MainRepo.GetCurrentFiadorIndex());
        }
    }

    private void EntryEdad_TextChanged(object sender, TextChangedEventArgs e)
    {
        paciente.Edad = EntryEdad.Text;
    }

    private async void BtnCancel2_Clicked(object sender, EventArgs e)
    {
        BtnCancel2.Opacity = 0;

        await BtnCancel2.FadeTo(1, 300);

        await Shell.Current.GoToAsync("..");
    }

    private async void BtnEdit_Clicked(object sender, EventArgs e)
    {
        BtnEdit.Opacity = 0;

        await BtnEdit.FadeTo(1, 300);
        enableEdit(true);
    }


    private async void BtnDelete_Clicked(object sender, EventArgs e)
    {
        BtnDelete.Opacity = 0;
        
        await BtnDelete.FadeTo(1, 300);
        bool confirm = await DisplayAlert("Eliminar", $"Desea eliminar al paciente: {paciente.Nombre}?", "No", "Si");
        if (!confirm)
        {
            var done = MainRepo.DeletePatient(paciente);
            if (done)
            {
                await DisplayAlert("Exito", $"Paciente eliminado: {paciente.Nombre}", "OK");
                await Shell.Current.GoToAsync("..");
            }
            else
                await DisplayAlert("Error", $"El paciente tiene admisiones registradas", "OK");
        }
        else
        {
            await DisplayAlert("Cancelado", "Operacion cancelada, no se ha borrado nada", "OK");
            return;
        }

    }

    //enable disable interface
    private void enableEdit(bool x)
    {
        entryName.IsEnabled = x;
        entryTelefono.IsEnabled = x;
        entryDomicilio.IsEnabled = x;
        entryNacionalidad.IsEnabled = x;
        entryEstado.IsEnabled = x;
        entryCiudad.IsEnabled = x;
        EntryEdad.IsEnabled = x;
        pickerEdoCivil.IsEnabled = x;
        pickerSexo.IsEnabled = x;
        BtnEdit.IsEnabled = !x;
        BtnEdit.IsVisible = !x;
        BtnGuardar.IsEnabled = x;
        BtnGuardar.IsVisible = x;
        BtnCancel.IsEnabled = x;
        BtnCancel.IsVisible = x;
        BtnCancel2.IsEnabled = !x;
        BtnCancel2.IsVisible = !x;
        BtnDelete.IsEnabled = !x;
        BtnDelete.IsVisible = !x;
        entryNombreCercano.IsEnabled = x;
        entryTelefonoCercano.IsEnabled = x;
        entryDomicilioCercano.IsEnabled = x;
        entryCiudadCercano.IsEnabled = x;
        entryEstadoCercano.IsEnabled = x;
        entryRelacionCercano.IsEnabled = x;
    }
}