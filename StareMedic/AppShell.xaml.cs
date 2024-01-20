using StareMedic.Views;
using StareMedic.Views.Viewers;

namespace StareMedic;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(Pacientes), typeof(Pacientes));
        Routing.RegisterRoute(nameof(Doctores), typeof(Doctores));
		Routing.RegisterRoute(nameof(Habitaciones), typeof(Habitaciones));
        Routing.RegisterRoute(nameof(RegisterClinicalCase), typeof(RegisterClinicalCase));
		Routing.RegisterRoute(nameof(PickPatientView), typeof(PickPatientView));
		Routing.RegisterRoute(nameof(SearchCC), typeof(SearchCC));
		Routing.RegisterRoute(nameof(ViewClinicalCase), typeof(ViewClinicalCase));
		Routing.RegisterRoute(nameof(MedicControll), typeof(MedicControll));
		Routing.RegisterRoute(nameof(PatientControll), typeof(PatientControll));
		Routing.RegisterRoute(nameof(RoomControll), typeof(RoomControll));

	
	}
}