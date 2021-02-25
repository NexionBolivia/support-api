using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Support.API.Services.Models.ModelConfigurations
{
    public class RoleToKoboUserConfiguration : IEntityTypeConfiguration<RoleToKoboUser>
    {
        public void Configure(EntityTypeBuilder<RoleToKoboUser> builder)
        {
            builder.HasKey(p => new { p.KoboUserId, p.RoleId });
            builder.Property(p => p.KoboUserId).IsRequired();
            builder.Property(p => p.RoleId).IsRequired();
            builder.HasOne(p => p.Role).WithMany(p => p.RoleToKoboUsers).HasForeignKey(p => p.RoleId);
        }
    }
}