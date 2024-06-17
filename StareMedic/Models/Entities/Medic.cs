namespace StareMedic.Models.Entities
{
    public class Medic
    {
        private readonly int _id;

        private string _name;

        private string _phone;//contmplate various phone numbers


        public Medic(int id)
        {
            _id = id;
        }

        public Medic(Medic original)
        {

           _id = original._id;
            _name = original._name;
            _phone = original._phone;
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

        //public string Correo
        //{
        //    get => _email;
        //    set => _email = value;
        //}

        



    }
}
