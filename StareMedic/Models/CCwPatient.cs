namespace StareMedic.Models
{
    internal class CCwPatient
    {
        private string _id;
        private int _iddb;
        private string _name;
        private string _patientName;
        private string _medic;
        private int? _SDKFolio;

        public CCwPatient(int iddb, int? sDKFolio, string id, string name, string patientName, string medic)
        {
            _iddb = iddb;
            _id = id;
            _name = name;
            _patientName = patientName;
            _medic = medic;
            _SDKFolio = sDKFolio;
        }

        public string Id { get { return _id; } set { _id = value; } }
        public string Nombre { get { return _name; } set { _name = value; } }
        public string PatientName { get { return _patientName; } set { _patientName = value; } }
        public string Medic { get { return _medic; } set { _medic = value; } }
        public int? SDKFolio { get { return _SDKFolio; } set { _SDKFolio = value; } }
        public int Iddb
        {
            get => _iddb;
            set => _iddb = value;
        }

    }
}
