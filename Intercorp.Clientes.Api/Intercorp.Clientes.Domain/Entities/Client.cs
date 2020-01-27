using System;
using System.Collections.Generic;
using System.Text;

namespace Intercorp.Clientes.Domain.Entities
{
    public class Client
    {
        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public int Edad { get; set;  }

        public DateTime FechaDeNacimiento { get; set; }
    }
}
