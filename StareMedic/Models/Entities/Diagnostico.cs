﻿namespace StareMedic.Models.Entities
{
    public class Diagnostico
    {
        private readonly int _id;
        private string _content;

        public Diagnostico(int id)
        {

            _content = "Pendiente...";
            _id = id;
        }

        public Diagnostico()
        {

        }

        public string Contenido
        {
            get => _content;
            set => _content = value;
        }

        public int Id
        {
            get => _id;
        }
    }
}
