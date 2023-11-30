namespace StareMedic;

using StareMedic.Views;
using StareMedic.Models;



public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(RegisterPatient), typeof(RegisterPatient));
		Routing.RegisterRoute(nameof(Pacientes), typeof(Pacientes));
		Routing.RegisterRoute(nameof(EditPatient), typeof(EditPatient));
		Routing.RegisterRoute(nameof(RegisterMedic), typeof(RegisterMedic));
		Routing.RegisterRoute(nameof(RegisterRoom), typeof(RegisterRoom));
		Routing.RegisterRoute(nameof(RegisterClinicalCase), typeof(RegisterClinicalCase));
		Routing.RegisterRoute(nameof(PickPatientView), typeof(PickPatientView));
		Routing.RegisterRoute(nameof(SearchCC), typeof(SearchCC));
		Routing.RegisterRoute(nameof(ViewClinicalCase), typeof(ViewClinicalCase));
	
	}
}