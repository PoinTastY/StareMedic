using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StareMedic.Models.Entities
{
    public class Fiador
    {
        //atributes
        private uint? _id;
        private string _name;
        private string _phone;
        private string _address;
        private string _city;
        private string _state;
        private string _relation;

        //builder
        public Fiador(uint id)
        {
            _id = id;
            _name = "";
            _phone = "";
            _address = "";
            _city = "";
            _state = "";
            _relation = "";

        }

        public Fiador() 
        {
            _id = null;
            _name = "";
            _phone = "";
            _address = "";
            _city = "";
            _state = "";
            _relation = "";

        }

        //methods
        public uint? Id
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

        public string Relacion
        {
            get => _relation;
            set => _relation = value;
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
