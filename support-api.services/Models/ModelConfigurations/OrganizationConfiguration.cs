using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace support_api.services.Models.ModelConfigurations
{
    public class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
    {
        public void Configure(EntityTypeBuilder<Organization> builder)
        {
            builder.HasKey(p => p.IdOrganization);
            builder.Property(p => p.IdOrganization).ValueGeneratedOnAdd().IsRequired();
            builder.Property(p => p.Name).HasMaxLength(500);
        }
    }
}