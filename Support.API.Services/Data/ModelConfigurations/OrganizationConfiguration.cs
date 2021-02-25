using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Support.API.Services.Models.ModelConfigurations
{
    public class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
    {
        public void Configure(EntityTypeBuilder<Organization> builder)
        {
            builder.HasKey(p => p.OrganizationId);
            builder.Property(p => p.OrganizationId).ValueGeneratedOnAdd().IsRequired();
            builder.Property(p => p.Name).HasMaxLength(500);
            builder.HasOne(p => p.Parent).WithMany(p => p.Children).HasForeignKey(p => p.OrganizationId).IsRequired(false);
        }
    }
}