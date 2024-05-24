namespace StareMedic.Models.Entities
{
    public class CasoClinico
    {
        private string _id;

        private readonly int _idDB;

        private string _name;

        private int _idPacient;

        private int _idDoctor;

        private int _idHabitacion;

        private int _idDiagnostico;

        private DateTimeOffset _fechaIngreso;

        private string _tipocaso;

        private double? _folioSDK;


        public CasoClinico(int id)
        {
            _idDB = id;
            _fechaIngreso = DateTimeOffset.UtcNow;
            _idDoctor = 0;
            _idHabitacion = 0;
            _idPacient = 0;
        }

        public CasoClinico() { }
        //Total account?
        //TO-DO: mplement a static method to assign all the values to the object?

        public int IdDB
        {
            get => _idDB;
        }

        public string Id
        {
            //the set, will be from a special format method, provided by the repo on build time
            get => _id;
            set => _id = value;
        }

        public string Nombre
        {
            get => _name;
            set => _name = value;
        }

        public int IdPaciente
        {
            get => _idPacient;
            set => _idPacient = value;
        }

        public int IdDoctor
        {
            get => _idDoctor;
            set => _idDoctor = value;
        }

        public int IdHabitacion
        {
            get => _idHabitacion;
            set => _idHabitacion = value;
        }

        public int IdDiagnostico//thisone will only be setted through the case overview, nowhere else
        {
            get => _idDiagnostico;
            set => _idDiagnostico = value;
        }

        public double? FolioSDK
        {
            get => _folioSDK;
            set => _folioSDK = value;
        }

        public string TipoCaso
        {
            get => _tipocaso;
            set => _tipocaso = value;
        }

        public DateTimeOffset FechaIngreso
        {
            get => _fechaIngreso.ToLocalTime();
            set => _fechaIngreso = value;
        }

        public void Update(CasoClinico caso)
        {
            _name = caso.Nombre;
            _idDoctor = caso.IdDoctor;
            _idHabitacion = caso.IdHabitacion;
        }

        public static implicit operator bool(CasoClinico x)
        {
            return (x._name != null || x._name != "") && x._idDoctor != 0 && x._idHabitacion != 0 && x._idPacient != 0 && !string.IsNullOrWhiteSpace(x._name);
        }

        public Patient Paciente()
        {
            return MainRepo.GetPatientById(_idPacient);
        }

        public Medic Medico()
        { 
            return MainRepo.GetMedicById(_idDoctor);
        }

        public Rooms Habitacion() 
        {             
            return MainRepo.GetRoomById(_idHabitacion);
        }

        public Diagnostico Diagnostico()
        {
                return MainRepo.GetDiagnosticoById(_idDiagnostico);
        }

        //maybe add close case method in here instead of repo?
    }
}