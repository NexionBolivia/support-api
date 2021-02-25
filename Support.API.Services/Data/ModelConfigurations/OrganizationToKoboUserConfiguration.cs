using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Support.API.Services.Models.ModelConfigurations
{
    public class OrganizationToKoboUserConfiguration : IEntityTypeConfiguration<OrganizationToKoboUser>
    {
        public void Configure(EntityTypeBuilder<OrganizationToKoboUser> builder)
        {
            builder.HasKey(p => new { p.KoboUserId, p.OrganizationId });
            builder.Property(p => p.KoboUserId).IsRequired();
            builder.Property(p => p.OrganizationId).IsRequired();
            builder.HasOne(p => p.Organization).WithMany(p => p.OrganizationToKoboUsers).HasForeignKey(p => p.OrganizationId);
        }
    }
}