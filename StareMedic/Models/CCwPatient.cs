using StareMedic.Models.Entities;

namespace StareMedic.Models
{
    internal class CCwPatient
    {
        private string _id;
        private int _iddb;
        private string _name;
        private string _patientName;
        private string _medic;
        private string _date;

        public CCwPatient(CasoClinico caso, Patient patient, Medic medic)
        {
            _iddb = caso.IdDB;
            _id = caso.Id;
            _name = caso.Nombre;
            _patientName = patient.Nombre;
            _medic = medic.Nombre;
            _date = caso.FechaIngreso.ToString("dd/MM/yyyy");
        }

        public string Date { get { return _date; } set { _date = value; } }

        public string Id { get { return _id; } set { _id = value; } }
        public string Nombre { get { return _name; } set { _name = value; } }
        public string PatientName { get { return _patientName; } set { _patientName = value; } }
        public string Medic { get { return _medic; } set { _medic = value; } }
        public int Iddb
        {
            get => _iddb;
            set => _iddb = value;
        }

    }
}
