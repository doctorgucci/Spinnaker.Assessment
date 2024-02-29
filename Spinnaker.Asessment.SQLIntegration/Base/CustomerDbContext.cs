using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Spinnaker.Asessment.SQLIntegration.Entities;
using Spinnaker.Asessment.SQLIntegration.Extensions;
using Spinnaker.Asessment.SQLIntegration.Mappings;
using System;

namespace Spinnaker.Asessment.SQLIntegration.Base
{
    public partial class CustomerDbContext(DbContextOptions<CustomerDbContext> options) : DbContext(options)
    {
        public virtual DbSet<CustomerEntity> Customers { get; set; }
        public virtual DbSet<CountriesEntity> Countries { get; set; }       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddConfiguration(new CustomerEntityMap());
            modelBuilder.AddConfiguration(new CountriesEntityMap());            

            base.OnModelCreating(modelBuilder);
        }
    }
}
