
using StareMedic.Views;
using StareMedic.Views.Viewers;

namespace StareMedic;

public partial class AppShell : Shell
{
    
    
	public AppShell()
	{
		InitializeComponent();
        //App
        Shell.SetBackButtonBehavior(this, new BackButtonBehavior { IsVisible = false } );
        //Routing
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

    private async void BtnPacientes_Clicked(object sender, EventArgs e)
    {
        
       PatientsFly.Opacity = 0;
       await PatientsFly.FadeTo(1, 200);

       await Shell.Current.GoToAsync(nameof(Pacientes));

    }


    /*private async void BtnRooms_Clicked(object sender, EventArgs e)
    {
        

        await Shell.Current.GoToAsync(nameof(Habitaciones));
    } */

    private async void BtnRegCasoCli_Clicked(object sender, EventArgs e)
    {
        RoomsFly.Opacity = 0;
        await RoomsFly.FadeTo(1, 200);

        await Shell.Current.GoToAsync(nameof(RegisterClinicalCase));
    }

    private async void BtnSearchCC_Clicked(object sender, EventArgs e)
    {
        CCFly.Opacity = 0;
        await CCFly.FadeTo(1, 200);

        await Shell.Current.GoToAsync(nameof(SearchCC));
    }

    private async void BtnMedics_Clicked(object sender, EventArgs e)
    {
        MedicsFly.Opacity = 0;
        await MedicsFly.FadeTo(1, 200);

        await Shell.Current.GoToAsync(nameof(Doctores));
    }

    private async void BtnAllBack_Clicked(object sender, EventArgs e)
    {
        MainMenu.Opacity = 0;

        await MainMenu.FadeTo(1, 200);
        await Shell.Current.GoToAsync("//MainPage");
    }
   


}