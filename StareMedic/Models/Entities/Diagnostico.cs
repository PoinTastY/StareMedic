using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StareMedic.Models.Entities
{
    public class Diagnostico
    {
        private readonly uint _id;
        private string _content;

        public Diagnostico(uint id)
        {

            _content = "Pendiente...";
            _id = id;
        }

        public string Contenido
        {
            get => _content;
            set => _content = value;
        }

        public uint Id
        {
            get => _id;
        }
    }
}
