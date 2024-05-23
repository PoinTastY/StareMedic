namespace StareMedic.Models
{
    internal class CCwPatient
    {
        private string _id;
        private int _iddb;
        private string _name;
        private string _patientName;

        public CCwPatient(int iddb, string id, string name, string patientName)
        {
            _iddb = iddb;
            _id = id;
            _name = name;
            if (patientName != null )
                _patientName = patientName;
        }

        public string Id { get { return _id; } set { _id = value; } }
        public string Nombre { get { return _name;} set { _name = value; } }
        public string PatientName { get { return _patientName; } set { _patientName = value; } }
        public int Iddb
        {
            get => _iddb;
            set => _iddb = value;
        }

    }
}
