using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StareMedic.Models.Entities
{
    public class Medic
    {
        private readonly int _id;

        private string _name;

        private string _phone;//contmplate various phone numbers

        //private string _email;//not forced rn


        public Medic(int id)
        {
            _id = id;
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
