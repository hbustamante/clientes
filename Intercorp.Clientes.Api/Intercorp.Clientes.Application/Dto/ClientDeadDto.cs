using System;
using System.Collections.Generic;
using System.Text;

namespace Intercorp.Clientes.Application.Dto
{
    public class ClientDeadDto
    {
        public ClientDto Cliente { get; set; }

        public DateTime FechaProbableMuerte { get; set; }
    }
}
