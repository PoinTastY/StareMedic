using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StareMedic.Models.Entities
{
    public class Medic
    {
        private readonly uint _id;

        private string _name;

        private string _phone;//contmplate various phone numbers

        //private string _email;//not forced rn


        public Medic(uint id)
        {
            _id = id;
        }

        public uint Id
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
