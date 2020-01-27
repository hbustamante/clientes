using System;
using System.Collections.Generic;
using System.Text;

namespace Intercorp.Clientes.Repository.Entities
{
    public class ClientDB: BaseEntityDB
    {
        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public DateTime FechaDeNacimiento { get; set; }
    }
}
