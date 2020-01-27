using Intercorp.Clientes.Application.Dto;
using Intercorp.Clientes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Intercorp.Clientes.Application.ApplicationServices.ClientServices
{
    public interface ICreateClientService
    {
        Task<Client> CreateClient(Client clientInfo);

        Task<ClientAverageDto> GetAverage();

        Task<ClientStandardDeviationDto> GetStandardDeviation();

        Task<ClientKpiDto> GetKpi();

        Task<List<ClientDeadDto>> GetClients();
    }
}
