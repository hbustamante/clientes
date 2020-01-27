using System;
using System.Collections.Generic;
using System.Text;

namespace Intercorp.Clientes.Application.Dto
{
    public class ClientKpiDto
    {
        public ClientAverageDto clientAverageDto { get; set; }
        public ClientStandardDeviationDto clientStandardDeviationDto { get; set; }
    }
}
