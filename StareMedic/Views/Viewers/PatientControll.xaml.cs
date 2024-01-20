using StareMedic.Models.Entities;
using StareMedic.Models;

namespace StareMedic.Views.Viewers;

public partial class PatientControll : ContentPage
{
    Patient paciente;
    Cercano cercano;
    Fiador fiador;
    readonly List<int> agelist = new();

    public PatientControll(Patient paciente)
	{
		InitializeComponent();

        if (paciente != null)
        {
            this.paciente = paciente;
            FillFields();
        }
        else
        {
            this.paciente = new(MainRepo.GetCurrentPatientIndex());
            paciente = new(MainRepo.GetCurrentPatientIndex());
            cercano = new(MainRepo.GetCurrentCercanoIndex());
            fiador = new(MainRepo.GetCurrentFiadorIndex());
        }

        //age thing
        for (int age = 0; age <= 120; age++)
            agelist.Add(age);
        pickerEdad.ItemsSource = agelist;
        pickerEdad.SelectedItem = 0;
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

    private void PickerSangre_SelectedIndexChanged(object sender, EventArgs e)
    {
        paciente.TipoSangre = (string)pickerSangre.SelectedItem;
    }

    private void PickerEdad_SelectedIndexChanged(object sender, EventArgs e)
    {
        //fix "Edad 0" bug
        paciente.Edad = (int)pickerEdad.SelectedItem;
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

    //Fiador
    private void EntryNombreFiador_TextChanged(object sender, TextChangedEventArgs e)
    {
        fiador.Nombre = entryNombreFiador.Text;
    }

    private void EntryTelefonoFiador_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(e.NewTextValue) && !ValidationHelper.IsNumeric(e.NewTextValue))
        {
            entryTelefonoFiador.Text = e.OldTextValue;
        }
        else
        {
            fiador.Telefono = entryTelefonoFiador.Text;
        }
    }

    private void EntryDireccionFiador_TextChanged(object sender, TextChangedEventArgs e)
    {
        fiador.Direccion = entryDireccionFiador.Text;
    }

    private void EntryCiudadFiador_TextChanged(object sender, TextChangedEventArgs e)
    {
        fiador.Ciudad = entryCiudadFiador.Text;
    }

    private void EntryEstadoFiador_TextChanged(object sender, TextChangedEventArgs e)
    {
        fiador.Estado = entryEstadoFiador.Text;
    }

    //confirm buttons
    private async void BtnCancel_Clicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Cancelar", "Desea cancelar el registro?", "No", "Si");
        if (!confirm)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
    private async void BtnGuardar_Clicked(object sender, EventArgs e)
    {
        if (paciente)
        {
            //the cancel and confirm buttons are switched bcs i like it that way
            bool confirmacion = await DisplayAlert("Confirmar", $"Se registrara a:\n{paciente.Nombre}", "Cancelar", "Confirmar");
            if (!confirmacion)//that is actually true lol
            {
                //add 2 repo
                if (cercano)
                {

                    paciente.IdCercano = cercano.Id;
                    MainRepo.AddCercano(cercano);
                    if (chkCercanoIsFiador.IsChecked)
                    {
                        fiador.Copy(cercano);
                        MainRepo.AddFiador(fiador);
                        paciente.IdFiador = fiador.Id;

                    }
                    else if (fiador.Nombre != null && fiador.Nombre != "")
                    {
                        paciente.IdFiador = fiador.Id;
                        MainRepo.AddFiador(fiador);
                    }
                    else if (fiador)
                    {
                        paciente.IdFiador = fiador.Id;
                        MainRepo.AddFiador(fiador);
                    }

                }
                if ((cercano.Nombre == null || cercano.Nombre == "") && (fiador) && !chkCercanoIsFiador.IsChecked)
                {

                    paciente.IdFiador = fiador.Id;
                    MainRepo.AddFiador(fiador);

                }

                MainRepo.AddPatient(paciente);
                await DisplayAlert("Exito", $"Paciente registrado: {paciente.Nombre}", "OK");
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

    private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (chkCercanoIsFiador.IsChecked == true)
        {
            gridFiador.IsVisible = false;
        }
        else
        {
            gridFiador.IsVisible = true;
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
        pickerEdad.SelectedItem = paciente.Edad;
        pickerEdoCivil.SelectedItem = paciente.EstadoCivil;
        pickerSangre.SelectedItem = paciente.TipoSangre;
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
            cercano = MainRepo.GetCercanoById(paciente.IdCercano);
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

        //fiador
        if (paciente.IdFiador != null)
        {
            fiador = MainRepo.GetFiadorById(paciente.IdFiador);
            entryNombreFiador.Text = fiador.Nombre;
            entryTelefonoFiador.Text = fiador.Telefono;
            entryDireccionFiador.Text = fiador.Direccion;
            entryCiudadFiador.Text = fiador.Ciudad;
            entryEstadoFiador.Text = fiador.Estado;
        }
        else
        {
            fiador = new(MainRepo.GetCurrentFiadorIndex());
        }
    }
}