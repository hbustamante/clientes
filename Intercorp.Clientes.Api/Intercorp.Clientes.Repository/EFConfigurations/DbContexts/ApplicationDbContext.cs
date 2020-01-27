using Intercorp.Clientes.Repository.EFConfigurations.FluentSetups;
using Intercorp.Clientes.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intercorp.Clientes.Repository.EFConfigurations.DbContexts
{
    public class ApplicationDbContext: DbContext
    {
        private readonly IConfiguration configuration;

        #region Constructor

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public ApplicationDbContext(IConfiguration configuration)
           : base()
        {
            this.configuration = configuration;
        }

        public ApplicationDbContext(IConfiguration configuration, DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            this.configuration = configuration;
        }

        #endregion

        #region DbSet Setups

        public DbSet<ClientDB> Clientes { get; set; }

        #endregion DbSet Setups

        #region SaveChanges
        public override int SaveChanges()
        {
            SetCreateDate();
            SetUpdateDate();
            SetLogicalDelete();

            return base.SaveChanges();
        }

        private void SetLogicalDelete()
        {
            foreach (var item in ChangeTracker.Entries().Where(e => e.State == EntityState.Deleted &&
             e.Metadata.GetProperties().Any(x => x.Name == "Deleted")))
            {
                item.State = EntityState.Unchanged;
                item.CurrentValues["Deleted"] = DateTime.Now;
            }
        }

        private void SetCreateDate()
        {
            foreach (var item in ChangeTracker.Entries().Where(e => e.State == EntityState.Added &&
             e.Metadata.GetProperties().Any(x => x.Name == "Created")))
            {
                item.CurrentValues["Created"] = DateTime.Now;
            }
        }

        private void SetUpdateDate()
        {
            foreach (var item in ChangeTracker.Entries().Where(e => e.State == EntityState.Modified &&
                         e.Metadata.GetProperties().Any(x => x.Name == "Modified")))
            {
                item.CurrentValues["Modified"] = DateTime.Now;
            }
        }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ClientMap());
        }
    }
}
