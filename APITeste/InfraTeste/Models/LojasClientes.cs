using System;
using System.Collections.Generic;
using System.Text;

namespace InfraTeste.Models
{
    public class LojasClientes
    {
        public int id { get; set; }
        public int idLoja { get; set; }
        public int idCliente { get; set; }

        public virtual Lojas loja { get; set; }
        public virtual Clientes cliente { get; set; }
    }
}
