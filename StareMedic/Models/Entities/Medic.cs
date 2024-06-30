namespace StareMedic.Models.Entities
{
    public class Medic
    {
        private readonly int _id;

        private string _name;

        private string _phone;

        private string _domicilio;

        private string _ciudad;

        private string _estado;


        public Medic(int id)
        {
            _id = id;
        }

        public Medic(Medic original)
        {

            _id = original._id;
            _name = original._name;
            _phone = original._phone;
            _domicilio = original._domicilio;
            _ciudad = original._ciudad;
            _estado = original._estado;
        }

        //default builder, dont use or move, if u need new builder, do another one
        public Medic()
        {
            _name = "missing";
        }

        public int Id
        {
            get => _id;
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

        public string Domicilio
        {
            get => _domicilio;
            set => _domicilio = value;
        }

        public string Ciudad
        {
            get => _ciudad;
            set => _ciudad = value;
        }

        public string Estado
        {
            get => _estado;
            set => _estado = value;
        }

        public static implicit operator bool(Medic medic)
        {
            if (!string.IsNullOrEmpty(medic._name) &&
                !string.IsNullOrEmpty(medic._phone) &&
               !string.IsNullOrEmpty(medic._domicilio) &&
               !string.IsNullOrEmpty(medic._ciudad) &&
               !string.IsNullOrEmpty(medic._estado))
            {
                return true;
            }
            return false;
        }
    }
}
