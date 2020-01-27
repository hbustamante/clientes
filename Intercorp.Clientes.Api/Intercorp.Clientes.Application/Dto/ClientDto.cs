using System;
using System.Collections.Generic;
using System.Text;

namespace Intercorp.Clientes.Application.Dto
{
    public class ClientDto
    {
        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public DateTime FechaDeNacimiento { get; set; }

        public int Edad { get; set; }
    }
}
