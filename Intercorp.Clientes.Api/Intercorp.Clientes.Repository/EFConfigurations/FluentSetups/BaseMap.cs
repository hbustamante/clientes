using Intercorp.Clientes.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Intercorp.Clientes.Repository.EFConfigurations.FluentSetups
{
    public abstract class BaseMap<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntityDB
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            OnConfigure(builder);
        }

        public abstract void OnConfigure(EntityTypeBuilder<TEntity> builder);
    }
}
