namespace StareMedic.Models.Entities
{
    public class Rooms
    {
        //Attributes
        private readonly int _id;
        private string _name;
        private string _description;//will be used as codigo cliente 4 contpaqsdk



        //builders
        public Rooms(int id)
        {
            _id = id;//from db autoincrement
        }

        public Rooms()
        {
            _name = "missing";
        }

        //methods
        public int Id
        {
            get => _id;
        }

        public string Nombre
        {
            get => _name;
            set => _name = value;
        }
        public string Descripcion
        {
            get => _description;
            set => _description = value;
        }

        public static implicit operator bool(Rooms room)
        {
            return string.IsNullOrEmpty(room._name);
        }
    }
}
