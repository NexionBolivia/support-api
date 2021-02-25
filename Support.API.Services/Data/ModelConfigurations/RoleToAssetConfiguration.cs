using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Support.API.Services.Models.ModelConfigurations
{
    public class RoleToAssetConfiguration : IEntityTypeConfiguration<RoleToAsset>
    {
        public void Configure(EntityTypeBuilder<RoleToAsset> builder)
        {
            builder.HasKey(p => new { p.RoleId, p.AssetId });
            builder.HasOne(p => p.Role).WithMany(p => p.RoleToAssets).HasForeignKey(p => p.RoleId);
            builder.HasOne(p => p.Asset).WithMany(p => p.RoleToAssets).HasForeignKey(p => p.AssetId);
        }
    }
}