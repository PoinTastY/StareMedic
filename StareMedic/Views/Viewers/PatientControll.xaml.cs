using StareMedic.Models.Entities;
using StareMedic.Models;

namespace StareMedic.Views.Viewers;

public partial class PatientControll : ContentView
{
    readonly Patient paciente = new(MainRepo.GetCurrentPatientIndex());
    readonly Cercano cercano = new(MainRepo.GetCurrentCercanoIndex());
    readonly Fiador fiador = new(MainRepo.GetCurrentFiadorIndex());
    readonly List<int> agelist = new();

    public PatientControll()
	{
		InitializeComponent();
        for (int age = 0; age <= 120; age++)
            agelist.Add(age);
        PickerEdad.ItemsSource = agelist;
    }

	public string PatientName
	{
		get
		{
			return EntryPatientName.Text;
		}
		set
		{
			EntryPatientName.Text = value;
		}
	}
		
	public char PatientSex
	{
		get
		{
            if (PickerSexo.SelectedIndex == 0)
            {
                return 'M';
            }
            else if (PickerSexo.SelectedIndex == 1)
            {
                return 'F';
            }
            else
            {
                return 'O';
            }
        }
		set
		{
			if(value == 'M')
			{
				PickerSexo.SelectedIndex = 0;
			}
			else if(value == 'F')
			{
				PickerSexo.SelectedIndex = 1;
			}
			else
			{
				PickerSexo.SelectedIndex = 2;
			}
		}
	}

	public string PatientCivilStatus
	{
		get
		{
			return (string)PickerEdoCivil.SelectedItem;
		}
		set
		{
			PickerEdoCivil.SelectedIndex = PickerEdoCivil.Items.ToList().FindIndex(x => x == value);
		}
	}

	public string PatientBloodType
	{
        get
		{
            return (string)PickerSangre.SelectedItem;
        }
        set
		{
            PickerSangre.SelectedIndex = PickerSangre.Items.ToList().FindIndex(x => x == value);
        }
    }

	public int PatientAge
	{
		get { return (int)PickerEdad.SelectedItem; }
		set { PickerEdad.SelectedIndex = agelist.FindIndex(x => x == value); }
	}

	public string PatientPhone
	{
		get
		{
            return EntryTelefono.Text;
        }
		set
		{
            EntryTelefono.Text = value;
        }
	}

    private void EntryPatientName_TextChanged(object sender, TextChangedEventArgs e)
    {

    }

    private void PickerSexo_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void PickerEdoCivil_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void PickerSangre_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void PickerEdad_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void EntryTelefono_TextChanged(object sender, TextChangedEventArgs e)
    {

    }

    private void entryDomicilio_TextChanged(object sender, TextChangedEventArgs e)
    {

    }

    private void EntryDomicilio_TextChanged_1(object sender, TextChangedEventArgs e)
    {

    }

    private void EntryCiudad_TextChanged(object sender, TextChangedEventArgs e)
    {

    }

    private void EntryEstado_TextChanged(object sender, TextChangedEventArgs e)
    {

    }

    private void EntryNacionalidad_TextChanged(object sender, TextChangedEventArgs e)
    {

    }

    private void EntryNombreCercano_TextChanged(object sender, TextChangedEventArgs e)
    {

    }

    private void EntryTelefonoCercano_TextChanged(object sender, TextChangedEventArgs e)
    {

    }

    private void EntryDomicilioCercano_TextChanged(object sender, TextChangedEventArgs e)
    {

    }

    private void EntryCiudadCercano_TextChanged(object sender, TextChangedEventArgs e)
    {

    }

    private void EntryEstadoCercano_TextChanged(object sender, TextChangedEventArgs e)
    {

    }

    private void EntryRelacionCercano_TextChanged(object sender, TextChangedEventArgs e)
    {

    }

    private void ChkCercanoIsFiador_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {

    }

    private void EntryNombreFiador_TextChanged(object sender, TextChangedEventArgs e)
    {

    }

    private void EntryTelefonoFiador_TextChanged(object sender, TextChangedEventArgs e)
    {

    }

    private void EntryDireccionFiador_TextChanged(object sender, TextChangedEventArgs e)
    {

    }

    private void EntryCiudadFiador_TextChanged(object sender, TextChangedEventArgs e)
    {

    }

    private void EntryEstadoFiador_TextChanged(object sender, TextChangedEventArgs e)
    {

    }

    private void BtnCancel_Clicked(object sender, EventArgs e)
    {

    }

    private void BtnGuardar_Clicked(object sender, EventArgs e)
    {

    }
}