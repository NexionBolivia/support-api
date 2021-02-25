using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Support.API.Services.Models.ModelConfigurations
{
    public class OrganizationProfileConfiguration : IEntityTypeConfiguration<OrganizationProfile>
    {
        public void Configure(EntityTypeBuilder<OrganizationProfile> builder)
        {
            builder.HasKey(p => p.ProfileId);
            builder.Property(p => p.ProfileId).ValueGeneratedOnAdd().IsRequired();
            builder.Property(p => p.Formation).HasMaxLength(500);
            builder.Property(p => p.Address).HasMaxLength(500);
            builder.Property(p => p.Phone).HasMaxLength(500);
            builder.Property(p => p.Department).HasMaxLength(500);
            builder.Property(p => p.Province).HasMaxLength(500);
            builder.Property(p => p.Municipality).HasMaxLength(500);
            builder.Property(p => p.ServiceContinuity).HasMaxLength(500);
            builder.HasOne(p => p.Organization).WithMany(p => p.Profiles).HasForeignKey(p => p.OrganizationId);
        }
    }
}