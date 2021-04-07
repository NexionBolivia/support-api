using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Support.API.Services.Models;

namespace Support.API.Services.Data
{
    internal class ApplicationDbContextBuilder
    {
        public virtual void BuildModel(ModelBuilder modelBuilder)
        {
            MapSyncRequestAsset(modelBuilder.Entity<Asset>());
            MapSyncRequestOrganization(modelBuilder.Entity<Organization>());
            MapSyncRequestOrganizationProfile(modelBuilder.Entity<OrganizationProfile>());
            MapSyncRequestOrganizationToKoboUser(modelBuilder.Entity<OrganizationToKoboUser>());
            MapSyncRequestRole(modelBuilder.Entity<Role>());
            MapSyncRequestRoleToAsset(modelBuilder.Entity<RoleToAsset>());
            MapSyncRequestRoleToKoboUser(modelBuilder.Entity<RoleToKoboUser>());
        }

        private void MapSyncRequestAsset(EntityTypeBuilder<Asset> builder)
        {
            builder.ToTable("Asset");
            builder.HasKey(p => p.AssetId);
            builder.Property(p => p.AssetId).ValueGeneratedOnAdd().IsRequired();
            builder.Property(p => p.Name).HasMaxLength(500);
            builder.Property(p => p.Path).HasMaxLength(500);
            builder.Property(p => p.Type).HasMaxLength(100);
            builder.HasOne(p => p.Parent).WithMany(p => p.Children).HasForeignKey(p => p.AssetId).IsRequired(false);
        }

        private void MapSyncRequestOrganization(EntityTypeBuilder<Organization> builder)
        {
            builder.ToTable("Organization");
            builder.HasKey(p => p.OrganizationId);
            builder.Property(p => p.OrganizationId).ValueGeneratedOnAdd().IsRequired();
            builder.Property(p => p.Name).HasMaxLength(500);
            builder.Property(p => p.Color).HasMaxLength(50);
            builder.HasOne(p => p.Parent).WithMany(p => p.Children).HasForeignKey(p => p.OrganizationId).IsRequired(false);
        }

        private void MapSyncRequestOrganizationProfile(EntityTypeBuilder<OrganizationProfile> builder)
        {
            builder.ToTable("OrganizationProfile");
            builder.HasKey(p => p.ProfileId);
            builder.Property(p => p.ProfileId).ValueGeneratedOnAdd().IsRequired();
            builder.Property(p => p.Formation).HasMaxLength(500);
            builder.Property(p => p.Address).HasMaxLength(500);
            builder.Property(p => p.Phone).HasMaxLength(500);
            builder.Property(p => p.Department).HasMaxLength(500);
            builder.Property(p => p.Province).HasMaxLength(500);
            builder.Property(p => p.Municipality).HasMaxLength(500);
            builder.Property(p => p.ServiceContinuity).HasMaxLength(500);
            builder.HasOne(p => p.Organization).WithOne(p => p.OrganizationProfile).HasForeignKey<Organization>(p => p.IdProfile).IsRequired(false);
        }

        private void MapSyncRequestOrganizationToKoboUser(EntityTypeBuilder<OrganizationToKoboUser> builder)
        {
            builder.ToTable("OrganizationToKoboUser");
            builder.HasKey(p => new { p.KoboUserId, p.OrganizationId });
            builder.Property(p => p.KoboUserId).IsRequired();
            builder.Property(p => p.OrganizationId).IsRequired();
            builder.HasOne(p => p.Organization).WithMany(p => p.OrganizationToKoboUsers).HasForeignKey(p => p.OrganizationId);
        }

        private void MapSyncRequestRole(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Role");
            builder.HasKey(p => p.RoleId);
            builder.Property(p => p.RoleId).ValueGeneratedOnAdd().IsRequired();
            builder.Property(p => p.Name).HasMaxLength(50);
        }

        private void MapSyncRequestRoleToAsset(EntityTypeBuilder<RoleToAsset> builder)
        {
            builder.ToTable("RoleToAsset");
            builder.HasKey(p => new { p.RoleId, p.AssetId });
            builder.HasOne(p => p.Role).WithMany(p => p.RoleToAssets).HasForeignKey(p => p.RoleId);
            builder.HasOne(p => p.Asset).WithMany(p => p.RoleToAssets).HasForeignKey(p => p.AssetId);
        }

        private void MapSyncRequestRoleToKoboUser(EntityTypeBuilder<RoleToKoboUser> builder)
        {
            builder.ToTable("RoleToKoboUser");
            builder.HasKey(p => new { p.KoboUserId, p.RoleId });
            builder.Property(p => p.KoboUserId).IsRequired();
            builder.Property(p => p.RoleId).IsRequired();
            builder.HasOne(p => p.Role).WithMany(p => p.RoleToKoboUsers).HasForeignKey(p => p.RoleId);
        }
    }
}
