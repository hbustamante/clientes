using Intercorp.Clientes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Intercorp.Clientes.Domain.Interfaces.Repositories
{
    public interface IClientRepository
    {
        Task Save(Client client);

        Task<List<Client>> GetClients();
    }
}
