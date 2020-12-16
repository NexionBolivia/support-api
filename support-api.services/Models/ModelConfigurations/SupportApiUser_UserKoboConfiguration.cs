using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupportAPI.Services.Models.ModelConfigurations
{
    public class SupportApiUser_UserKoboConfiguration : IEntityTypeConfiguration<SupportApiUser_UserKobo>
    {
        public void Configure(EntityTypeBuilder<SupportApiUser_UserKobo> builder)
        {
            builder.HasKey(p => new { p.Name, p.Username });
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.Username).IsRequired();
            builder.HasOne(p => p.SupportApiUser).WithMany(p => p.SupportApiUser_UserKobo).HasForeignKey(p => p.Username);
            builder.HasOne(p => p.UserKobo).WithMany(p => p.SupportApiUser_UserKobo).HasForeignKey(p => p.Name);
        }
    }
}