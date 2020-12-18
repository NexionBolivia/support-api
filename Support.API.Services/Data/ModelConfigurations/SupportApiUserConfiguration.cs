using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Support.API.Services.Models.ModelConfigurations
{
    public class SupportApiUserConfiguration : IEntityTypeConfiguration<SupportApiUser>
    {
        public void Configure(EntityTypeBuilder<SupportApiUser> builder)
        {
            builder.HasKey(p => p.Username);
            builder.Property(p => p.Username).IsRequired();
            builder.Property(p => p.Username).HasMaxLength(500);
            builder.Property(p => p.Password).HasMaxLength(500);
            builder.HasOne(p => p.Organization).WithMany(p => p.SupportApiUsers).HasForeignKey(p => p.IdOrganization);
        }
    }
}