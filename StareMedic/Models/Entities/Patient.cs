﻿namespace StareMedic.Models.Entities
{
    public class Patient
    {
        //Atributos paciente
        private int _id;//dbfilled only on creation
        private string _name;
        private string _domicilio;
        private string _ciudad;
        private string _estado;
        private string _nacionalidad;
        private string _estadoCivil;
        private char _sexo;
        private string _edad;
        private string _telefono;

        //controll info
        private DateTimeOffset _registered;

        //Contactos extras
        private int? _idCercano;
        private int? _idFiador;//Contpaq SDK comming soon here?

        //id initialization
        public Patient(int id)
        {
            _registered = DateTimeOffset.UtcNow;
            _id = id;
            _idCercano = null;
            _idFiador = null;
            //initiate w default values for notnull compatibility
            _name = "";
            _domicilio = "";
            _ciudad = "";
            _estado = "";
            _nacionalidad = "";
            _estadoCivil = "";
            _sexo = 'N';
            _edad = "";
            _telefono = "";
        }

        public Patient()
        {
        }

        //methods
        public int Id
        {
            get => _id;
            set => _id = value;
        }
        public string Nombre
        {
            get => _name;
            set => _name = value;
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

        public string Nacionalidad
        {
            get => _nacionalidad;
            set => _nacionalidad = value;
        }

        public string EstadoCivil
        {
            get => _estadoCivil;
            set => _estadoCivil = value;
        }

        public char Sexo
        {
            get => _sexo;
            set => _sexo = value;
        }

        public string Edad
        {
            get => _edad;
            set => _edad = value;
        }

        public string Telefono
        {
            get => _telefono;
            set => _telefono = value;
        }

        public DateTimeOffset Registered
        {
            get => _registered;
        }

        //dejo estos dos como la otra forma de declararse tambien
        public int? IdCercano
        {
            get { return _idCercano; }
            set { _idCercano = value; }
        }

        public int? IdFiador
        {
            get { return _idFiador; }
            set { _idFiador = value; }
        }

        public static implicit operator bool(Patient patient)
        {
            return !string.IsNullOrEmpty(patient._name);
        }

        public void Update(Patient patient)
        {
            //Update changes //honestl, idont remember why dis // this was bcs i wanted to copy it without having the issue with the id or something like that
            _name = patient._name;
            _domicilio = patient._domicilio;
            _ciudad = patient._ciudad;
            _estado = patient._estado;
            _nacionalidad = patient._nacionalidad;
            _estadoCivil = patient._estadoCivil;
            _sexo = patient._sexo;
            _edad = patient._edad;
            _telefono = patient._telefono;
            _registered = patient._registered;
            _idCercano = patient._idCercano;
            _idFiador = patient._idFiador;

        }

        public string SubDomicilio()
        {
            if (_domicilio == null)
            {
                return _domicilio;
            }
            return _domicilio.Length > 43 ? string.Concat(_domicilio.AsSpan(0, 40), "...") : _domicilio;
        }


        public Cercano Cercano
        {
            get {
                if (_idCercano == 0)
                {
                    return new Cercano(MainRepo.GetCurrentCercanoIndex());
                }
                else
                {
                    return MainRepo.GetCercanoById(_idCercano);
                }
            }
        }

        public Fiador Fiador()
        {
            if (_idFiador == 0)
            {
                return new Fiador(MainRepo.GetCurrentFiadorIndex());
            }
            else
            {
                return MainRepo.GetFiadorById(_idFiador);
            }
        }

        
    }
}
