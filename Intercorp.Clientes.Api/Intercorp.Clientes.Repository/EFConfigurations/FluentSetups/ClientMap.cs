using Intercorp.Clientes.Repository.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Intercorp.Clientes.Repository.EFConfigurations.FluentSetups
{
    public class ClientMap : BaseMap<ClientDB>
    {
        public override void OnConfigure(EntityTypeBuilder<ClientDB> builder)
        {
            builder.HasKey(claim => claim.Id);
            builder.HasQueryFilter(x => x.Deleted == null);
        }
    }
}
