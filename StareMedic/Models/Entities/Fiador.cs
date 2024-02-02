namespace StareMedic.Models.Entities
{
    public class Fiador
    {
        //atributes
        private int? _id;
        private string _name;
        private string _phone;
        private string _address;
        private string _city;
        private string _state;

        //builder
        public Fiador(int id)
        {
            _id = id;
            _name = "";
            _phone = "";
            _address = "";
            _city = "";
            _state = "";

        }

        public Fiador() 
        {
            _id = null;
            _name = "";
            _phone = "";
            _address = "";
            _city = "";
            _state = "";

        }

        //methods
        public int? Id
        {
            get => _id;
            set => _id = value;
        }

        public string Nombre
        {
            get => _name;
            set => _name = value;
        }

        public string Telefono
        {
            get => _phone;
            set => _phone = value;
        }

        public string Direccion
        {
            get => _address;
            set => _address = value;
        }

        public string Ciudad
        {
            get => _city;
            set => _city = value;
        }

        public string Estado
        {
            get => _state;
            set => _state = value;
        }


        public void Copy(Cercano objCercano)
        {
            _name = objCercano.Nombre;
            _phone = objCercano.Telefono;
            _address = objCercano.Direccion;
            _city = objCercano.Ciudad;
            _state = objCercano.Estado;
        }

        public static implicit operator bool(Fiador fiador)
        {
            return fiador._name != null && fiador._name != "";
        }
    }
}
