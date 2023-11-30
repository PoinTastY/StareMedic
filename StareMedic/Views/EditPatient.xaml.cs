using StareMedic.Models;

using Patient = StareMedic.Models.Entities.Patient;
using Cercano = StareMedic.Models.Entities.Cercano;
using Fiador = StareMedic.Models.Entities.Fiador;


namespace StareMedic.Views;

[QueryProperty(nameof(PatientID), "Id")]
[QueryProperty(nameof(CercanoID), "Id")]
[QueryProperty(nameof(FiadorID), "Id")]
public partial class EditPatient : ContentPage
{
    private Patient patient;
    private Patient originalPatient;

    private Cercano cercano;
    private Cercano originalCercano;

    private Fiador fiador;
    private Fiador originalFiador;

    public EditPatient()
    {
        InitializeComponent();

        Patient patient = new Patient();
        Cercano cecano = new Cercano();
        Fiador fiador = new Fiador();

        BindingContext = patient;
        BindingContext = cecano;
        BindingContext = fiador;

        pickerSangre.SelectedIndexChanged += PickerSangre_SelectedIndexChanged;
        pickerEdoCivil.SelectedIndexChanged += PickerEdoCivil_SelectedIndexChanged;
        pickerSexo.SelectedIndexChanged += PickerSexo_SelectedIndexChanged;
        pickerEdad.SelectedIndexChanged += PickerEdad_SelectedIndexChanged;
    }


    public string PatientID
    {
        set
        {
            patient = MainRepo.GetPatientById(uint.Parse(value));

            originalPatient = new Patient
            {
                Nombre = patient.Nombre,
                Domicilio = patient.Domicilio,
                TipoSangre = patient.TipoSangre,
                Ciudad = patient.Ciudad,
                Estado = patient.Estado,
                Nacionalidad = patient.Nacionalidad,
                EstadoCivil = patient.EstadoCivil,
                Sexo = patient.Sexo,
                Edad = patient.Edad,
                Telefono = patient.Telefono
            };

            BindingContext = patient;

            entryName.Text = patient.Nombre;
            entryDomicilio.Text = patient.Domicilio;
            entryCiudad.Text = patient.Ciudad;
            entryEstado.Text = patient.Estado;
            entryNacionalidad.Text = patient.Nacionalidad;
            entryTelefono.Text = patient.Telefono;


        }
    }

    public string CercanoID
    {
        set
        {
            cercano = MainRepo.GetCercanoById(uint.Parse(value));

            originalCercano = new Cercano
            {
                Nombre = cercano.Nombre,
                Telefono = cercano.Telefono,
                Direccion = cercano.Direccion,
                Ciudad = cercano.Ciudad,
                Estado = cercano.Estado,
                Relacion = cercano.Relacion
            };

            BindingContext = cercano;

            entryNombreCercano.Text = cercano.Nombre;
            entryTelefonoCercano.Text = cercano.Telefono;
            entryDomicilioCercano.Text = cercano.Direccion;
            entryCiudadCercano.Text = cercano.Ciudad;
            entryEstadoCercano.Text = cercano.Estado;
            entryRelacionCercano.Text = cercano.Relacion;


        }
    }

    public string FiadorID
    {
        set
        {
            fiador = MainRepo.GetFiadorById(uint.Parse(value));

            originalFiador = new Fiador

            {
                Nombre = fiador.Nombre,
                Telefono = fiador.Telefono,
                Direccion = fiador.Direccion,
                Ciudad = fiador.Ciudad,
                Estado = fiador.Estado,
            };

            BindingContext = fiador;

            entryNombreFiador.Text = fiador.Nombre;
            entryTelefonoFiador.Text = fiador.Telefono;
            entryDireccionFiador.Text = fiador.Direccion;
            entryCiudadFiador.Text = fiador.Ciudad;
            entryEstadoFiador.Text = fiador.Estado;


        }
    }

    //Patient
    private void EntryName_TextChanged(object sender, TextChangedEventArgs e)
    {  
         originalPatient.Nombre = entryName.Text;
    }

    private void EntryTelefono_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(e.NewTextValue) && !ValidationHelper.IsNumeric(e.NewTextValue))
        {
            entryTelefono.Text = e.OldTextValue;
        }
        else
        {
            originalPatient.Telefono = entryTelefono.Text;
        }
    }

 
    private void
        EntryDomicilio_TextChanged(object sender, TextChangedEventArgs e)
    {
        originalPatient.Domicilio = entryDomicilio.Text;
    }

    private void EntryNacionalidad_TextChanged(object sender, TextChangedEventArgs e)
    {
        originalPatient.Nacionalidad = entryNacionalidad.Text;
    }

    private void EntryEstado_TextChanged(object sender, TextChangedEventArgs e)
    {
        originalPatient.Estado = entryEstado.Text;
    }

    private void EntryCiudad_TextChanged(object sender, TextChangedEventArgs e)
    {
        originalPatient.Ciudad = entryCiudad.Text;
    }



    private void PickerSangre_SelectedIndexChanged(object sender, EventArgs e)
    {
        originalPatient.TipoSangre = (string)pickerSangre.SelectedItem;
    }

    private void PickerEdoCivil_SelectedIndexChanged(object sender, EventArgs e)
    {
        originalPatient.EstadoCivil = (string)pickerEdoCivil.SelectedItem;
    }

    private void PickerSexo_SelectedIndexChanged(Object sender, EventArgs e) 
    {
        if (pickerSexo.SelectedIndex == 0)
        {
            originalPatient.Sexo = 'M';
        }
        else if (pickerSexo.SelectedIndex == 1)
        {
            originalPatient.Sexo = 'F';
        }
        else
        {
            originalPatient.Sexo = 'O';
        }
    }

    private void PickerEdad_SelectedIndexChanged(Object sender, EventArgs e)
    {
        string selectedEdad = pickerEdad.SelectedItem?.ToString();
        if (!string.IsNullOrEmpty(selectedEdad) && int.TryParse(selectedEdad, out int edad))
        {
            originalPatient.Edad = edad;
        }
    }


    //Cercano
    private void EntryNombreCercano_TextChanged(object sender, TextChangedEventArgs e)
    {
        originalCercano.Nombre = entryNombreCercano.Text;
    }

    private void EntryTelefonoCercano_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(e.NewTextValue) && !ValidationHelper.IsNumeric(e.NewTextValue))
        {
            entryTelefonoCercano.Text = e.OldTextValue;
        }
        else
        {
            originalCercano.Telefono = entryTelefonoCercano.Text;
        }

    }

    private void EntryDomicilioCercano_TextChanged(object sender, TextChangedEventArgs e)
    {
        originalCercano.Direccion = entryDomicilioCercano.Text;
    }

    private void EntryCiudadCercano_TextChanged(object sender, TextChangedEventArgs e)
    {
        originalCercano.Ciudad = entryCiudadCercano.Text;
    }

    private void EntryEstadoCercano_TextChanged(object sender, TextChangedEventArgs e)
    {
        originalCercano.Estado = entryEstadoCercano.Text;
    }

    private void EntryRelacionCercano_TextChanged(object sender, TextChangedEventArgs e)
    {
        originalCercano.Relacion = entryRelacionCercano.Text;
    }

    //Fiador
    private void EntryNombreFiador_TextChanged(object sender, TextChangedEventArgs e)
    {
        originalFiador.Nombre = entryNombreFiador.Text;
    }

    private void EntryTelefonoFiador_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(e.NewTextValue) && !ValidationHelper.IsNumeric(e.NewTextValue))
        {
            entryTelefonoFiador.Text = e.OldTextValue;
        }
        else
        {
            originalFiador.Telefono = entryTelefonoFiador.Text;
        }
    }

    private void EntryDireccionFiador_TextChanged(object sender, TextChangedEventArgs e)
    {
        originalFiador.Direccion = entryDireccionFiador.Text;
    }

    private void EntryCiudadFiador_TextChanged(object sender, TextChangedEventArgs e)
    {
        originalFiador.Ciudad = entryCiudadFiador.Text;
    }

    private void EntryEstadoFiador_TextChanged(object sender, TextChangedEventArgs e)
    {
        originalFiador.Estado = entryEstadoFiador.Text;
    }

    private async void BtnCancel_Clicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Cancelar", "Desea cancelar la edicion del placiente?", "No", "Si");

        if (!confirm)
        {
            //for patient

            entryName.Text = originalPatient.Nombre;
            entryDomicilio.Text = originalPatient.Domicilio;
            entryCiudad.Text = originalPatient.Ciudad;
            entryEstado.Text = originalPatient.Estado;
            entryNacionalidad.Text = originalPatient.Nacionalidad;
            entryTelefono.Text = originalPatient.Telefono;
            pickerSangre.SelectedItem = originalPatient.TipoSangre;
            pickerEdoCivil.SelectedItem = originalPatient.EstadoCivil;
            if (originalPatient.Sexo == 'M')
            {
                pickerSexo.SelectedIndex = 0;
            }
            else if (originalPatient.Sexo == 'F')
            {
                pickerSexo.SelectedIndex = 1;
            }
            else
            {
                pickerSexo.SelectedIndex = 2;
            }
            pickerEdad.SelectedIndex = originalPatient.Edad;

            //for cercano

            entryNombreCercano.Text = originalCercano.Nombre;
            entryTelefonoCercano.Text = originalCercano.Telefono;
            entryDomicilioCercano.Text = originalCercano.Direccion;
            entryCiudadCercano.Text = originalCercano.Ciudad;
            entryEstadoCercano.Text = originalCercano.Estado;
            entryRelacionCercano.Text = originalCercano.Relacion;

            //for fiador 

            entryNombreFiador.Text = originalFiador.Nombre;
            entryTelefonoFiador.Text = originalFiador.Telefono;
            entryDireccionFiador.Text = originalFiador.Direccion;
            entryCiudadFiador.Text = originalFiador.Ciudad;
            entryEstadoFiador.Text = originalFiador.Estado;



            await Shell.Current.GoToAsync("..");
        }
        
    }


    private async void BtnSave_Clicked(object sender, EventArgs e)
    {
       

        if (originalPatient.Nombre != null && originalPatient.Nombre != "")
        {
            //the cancel and confirm buttons are switched bcs i like it that way
            bool confirmacion = await DisplayAlert("Confirmar", $"Se registrara a:\n{originalPatient.Nombre}", "Cancelar", "Confirmar");
            if (!confirmacion)//that is actually true lol
            {
                //add 2 repo
                if (originalCercano.Nombre != null && cercano.Nombre != "")
                {

                    //*if (chkCercanoIsFiador.IsChecked)
                    //{
                        //originalFiador.Copy(originalCercano);
                        //MainRepo.AddFiador(originalFiador);                     
                   // } 
                }

                MainRepo.UpdatePatient(originalPatient);
                await DisplayAlert("Exito", $"Paciente Editato: {originalPatient.Nombre}", "OK");
                //go back
                await Shell.Current.GoToAsync("..");
            }
        }
        else
        {
            await DisplayAlert("Error:", "No se puede editar un paciente sin nombre", "OK");
        }


        await Shell.Current.GoToAsync("..");
    }


}