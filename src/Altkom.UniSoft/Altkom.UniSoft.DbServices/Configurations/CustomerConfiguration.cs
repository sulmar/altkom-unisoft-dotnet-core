using Altkom.UniSoft.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Altkom.UniSoft.DbServices.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(p => p.FirstName).IsRequired().HasMaxLength(40);
            builder.Property(p => p.LastName).IsRequired().HasMaxLength(40);
            builder.Property(p => p.Email).IsUnicode(false).HasMaxLength(250);
            builder.Property(p => p.City).HasMaxLength(50);
        }
    }
}
