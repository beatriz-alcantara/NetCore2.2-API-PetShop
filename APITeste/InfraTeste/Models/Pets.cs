using System;
using System.Collections.Generic;
using System.Text;

namespace InfraTeste.Models
{
    public class Pets
    {
        public int id { get; set; }
        public string nome { get; set; }
        public int idCliente { get; set; }
        public string especie { get; set; }
        public bool isAtivo { get; set; }


        public virtual Clientes cliente { get; set; }
    }
}
