using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Support.API.Services.Models.ModelConfigurations
{
    public class AssetConfiguration : IEntityTypeConfiguration<Asset>
    {
        public void Configure(EntityTypeBuilder<Asset> builder)
        {
            builder.HasKey(p => p.AssetId);
            builder.Property(p => p.AssetId).ValueGeneratedOnAdd().IsRequired();
            builder.Property(p => p.Name).HasMaxLength(500);
            builder.Property(p => p.Path).HasMaxLength(500);
            builder.Property(p => p.Type).HasMaxLength(100);
            builder.HasOne(p => p.Parent).WithMany(p => p.Children).HasForeignKey(p => p.AssetId).IsRequired(false);
        }
    }
}