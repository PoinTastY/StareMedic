using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StareMedic.Models.Entities
{
    public class Rooms
    {
        //Attributes
        private readonly int _id;
        private string _name;
        private string _description;

        //maybe need a quantity of patients that can be in the room?
        private bool _status;

        //builders
        public Rooms(int id)
        {
            _id = id;//from db autoincrement
            _status = false;
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


        public bool Status
        {
            get => _status;
            set => _status = value;
        }
    }
}
