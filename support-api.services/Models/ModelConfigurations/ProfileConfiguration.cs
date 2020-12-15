using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace support_api.services.Models.ModelConfigurations
{
    public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
    {
        public void Configure(EntityTypeBuilder<Profile> builder)
        {
            builder.HasKey(p => p.IdProfile);
            builder.Property(p => p.IdProfile).ValueGeneratedOnAdd().IsRequired();
            builder.Property(p => p.Formation).HasMaxLength(500);
            builder.Property(p => p.Address).HasMaxLength(500);
            builder.Property(p => p.Phone).HasMaxLength(500);
            builder.Property(p => p.Department).HasMaxLength(500);
            builder.Property(p => p.Province).HasMaxLength(500);
            builder.Property(p => p.Municipality).HasMaxLength(500);
            builder.Property(p => p.ServiceContinuity).HasMaxLength(500);
            builder.HasOne(p => p.Organization).WithOne(p => p.Profile).HasForeignKey<Organization>(p => p.IdProfile);
        }
    }
}