using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupportAPI.Services.Models.ModelConfigurations
{
    public class UserKoboConfiguration : IEntityTypeConfiguration<UserKobo>
    {
        public void Configure(EntityTypeBuilder<UserKobo> builder)
        {
            builder.HasKey(p => p.Name);
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.Password).HasMaxLength(500);
        }
    }
}