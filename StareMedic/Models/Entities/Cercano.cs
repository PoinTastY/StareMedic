﻿namespace StareMedic.Models.Entities
{
    public class Cercano
    {
        //atributes
        private int? _id;
        private string _name;
        private string _phone;
        private string _address;
        private string _city;
        private string _state;
        private string _relation;

        //builder
        public Cercano(int id)
        {
            _id = id;
            _name = "";
            _phone = "";
            _address = "";
            _city = "";
            _state = "";
            _relation = "";

        }

        public Cercano(Cercano original)
        {
            _id = original._id;
            _name = original._name;
            _phone = original._phone;
            _address = original._address;
            _city = original._city;
            _state = original._state;
            _relation = original._relation;
        }

        public Cercano()
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

        public string Relacion
        {
            get => _relation;
            set => _relation = value;
        }

        public static implicit operator bool(Cercano cercano)
        {
            if (!string.IsNullOrEmpty(cercano._name) &&
               !string.IsNullOrEmpty(cercano._address) &&
               !string.IsNullOrEmpty(cercano._relation) &&
               cercano._id != null)

            {
                return true;
            }
            return false;
        }

        public string SubDireccion()
        {
            if (_address == null)
            {
                return "";
            }
            return _address.Length > 43 ? string.Concat(_address.AsSpan(0, 40), "...") : _address;
        }

    }
}
