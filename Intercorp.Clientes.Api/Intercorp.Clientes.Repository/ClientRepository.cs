using Intercorp.Clientes.Domain.Entities;
using Intercorp.Clientes.Domain.Interfaces.Repositories;
using Intercorp.Clientes.Repository.EFConfigurations.DbContexts;
using Intercorp.Clientes.Repository.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intercorp.Clientes.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ClientRepository(
            ApplicationDbContext applicationDbContext)
        {
            this._applicationDbContext = applicationDbContext;
        }

        public async Task<Client> GetClient(Client client)
        {
            var clientFilter = _applicationDbContext.Clientes.Where(c => c.Nombre.ToUpper() == client.Nombre.ToUpper() 
                                                                    && c.Apellido.ToUpper() == client.Apellido.ToUpper()).FirstOrDefault();
            return clientFilter.Adapt<Client>();
        }

        public async Task<List<Client>> GetClients()
        {
            var listClientAverage = _applicationDbContext.Clientes.Where(c => !c.IsDeleted).ToList();
            return listClientAverage.Adapt<List<Client>>();
        }

        public async Task Save(Client client)
        {
            var clientToSave = client.Adapt<ClientDB>();
            _applicationDbContext.Clientes.Add(clientToSave);
            _applicationDbContext.SaveChanges();
        }
    }
}
