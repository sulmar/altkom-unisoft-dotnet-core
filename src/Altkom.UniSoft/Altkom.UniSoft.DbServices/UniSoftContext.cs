using Altkom.UniSoft.DbServices.Configurations;
using Altkom.UniSoft.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Altkom.UniSoft.DbServices
{
    public class UniSoftContext : DbContext
    {
        public UniSoftContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Service> Services { get; set; }

        // public DbSet<Item> Items { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new ItemConfiguration());

            // modelBuilder.ApplyConfigurationsFromAssembly()

            base.OnModelCreating(modelBuilder);
        }
    }
}
