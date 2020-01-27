using Intercorp.Clientes.Application.ApplicationServices.ClientServices;
using Intercorp.Clientes.Application.Dto;
using Intercorp.Clientes.Domain.Entities;
using Intercorp.Clientes.Domain.Interfaces.Repositories;
using Intercorp.Clientes.Repository.EFConfigurations.DbContexts;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intercorp.Clientes.Application.ApplicationProviders.ClientServices
{
    public class CreateClientService : ICreateClientService
    {

        private readonly IClientRepository _clientRepository;

        public CreateClientService(
            IClientRepository clientRepository)
        {
            this._clientRepository = clientRepository;
        }


        public async Task<Client> CreateClient(Client clientInfo)
        {
            var client = new Client
            {
                Apellido = clientInfo.Apellido,
                Nombre = clientInfo.Nombre,
                FechaDeNacimiento = clientInfo.FechaDeNacimiento,
                Edad = clientInfo.Edad
                
            };

            await _clientRepository.Save(client);
            return client;
        }

        #region Average
        public async Task<ClientAverageDto> GetAverage()
        {
            List<Client> clientes = await _clientRepository.GetClients();
            return CalcularPromedio(clientes);
        }

        private ClientAverageDto CalcularPromedio(List<Client> clientes)
        {
            List<int> edades = clientes.Select(c => CalcularEdad(c.FechaDeNacimiento)).ToList();
            double promedioEdad = edades.Average();

            var clientDto = new ClientAverageDto
            {
                EdadPromedio = promedioEdad,
                MensajeEdadPromedio = string.Format("El promedio de edad de los clientes es: {0}", promedioEdad)
            };

            return clientDto;
        }

        private int CalcularEdad(DateTime fechaDeNacimiento)
        {
            DateTime now = DateTime.Now;
            int edad = 0;
            edad = now.Year - fechaDeNacimiento.Year;
            if (now.Month < fechaDeNacimiento.Month || (now.Month == fechaDeNacimiento.Month && now.Day < fechaDeNacimiento.Day))
                edad--;
            return edad;
        }

        #endregion

        #region StandardDeviation
        public async Task<ClientStandardDeviationDto> GetStandardDeviation()
        {
            List<Client> clientes = await _clientRepository.GetClients();
            return CalcularDesviacion(clientes);
        }

        private ClientStandardDeviationDto CalcularDesviacion(List<Client> clientes)
        {
            List<int> edades = clientes.Select(c => CalcularEdad(c.FechaDeNacimiento)).ToList();
            double desviacionStandard = StandardDeviation(edades);

            var clientSDDto = new ClientStandardDeviationDto
            {
                DesviacionStandard = desviacionStandard,
                MensajeDesviacionStandard = string.Format("La desviación estándar de edad de los clientes es: {0}", desviacionStandard)
            };

            return clientSDDto;
        }

        public double StandardDeviation(List<int> valueList)
        {
            double average = valueList.Average();
            double sumOfSquaresOfDifferences = valueList.Select(val => (val - average) * (val - average)).Sum();
            double sd = Math.Sqrt(sumOfSquaresOfDifferences / valueList.Count());
            return sd;
        }


        #endregion

        #region ListadoClientes

        public async Task<List<ClientDeadDto>> GetClients()
        {
            List<ClientDeadDto> clientsDeadDto = new List<ClientDeadDto>();

            List<Client> clientes = await _clientRepository.GetClients();
            foreach (var client in clientes)
            {
                ClientDeadDto clientDeadDto = new ClientDeadDto();
                clientDeadDto.Cliente = client.Adapt<ClientDto>();
                clientDeadDto.Cliente.Edad = CalcularEdad(client.FechaDeNacimiento);
                clientDeadDto.FechaProbableMuerte = CalcularFechaMuerte(client);
                clientsDeadDto.Add(clientDeadDto);
            }

            return clientsDeadDto;
        }

        private DateTime CalcularFechaMuerte(Client client)
        {
            Random gen = new Random();
            double esperanzaDeVida = 80.5;
            double añosEstimados = esperanzaDeVida - CalcularEdad(client.FechaDeNacimiento);
            DateTime fechaProbableMuerte = client.FechaDeNacimiento.AddYears(Convert.ToInt32(añosEstimados));
            fechaProbableMuerte = fechaProbableMuerte.AddMonths(gen.Next(1,12));
            fechaProbableMuerte = fechaProbableMuerte.AddDays(gen.Next(1, 30));
            
            return fechaProbableMuerte;
        }


        #endregion

        public async Task<ClientKpiDto> GetKpi()
        {
            List<Client> clientes = await _clientRepository.GetClients();
            var clientKpi = new ClientKpiDto
            {
                clientAverageDto = CalcularPromedio(clientes),
                clientStandardDeviationDto = CalcularDesviacion(clientes)
            };

            return clientKpi;
        }

    }
}
