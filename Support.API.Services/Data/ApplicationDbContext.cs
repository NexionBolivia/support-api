using Microsoft.EntityFrameworkCore;
using Support.API.Services.Models;
using Support.API.Services.Models.ModelConfigurations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Support.API.Services.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
            //this.Database.EnsureCreated();
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AssetConfiguration());
            modelBuilder.ApplyConfiguration(new OrganizationConfiguration());
            modelBuilder.ApplyConfiguration(new OrganizationProfileConfiguration());
            modelBuilder.ApplyConfiguration(new OrganizationToKoboUserConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new RoleToAssetConfiguration());
            modelBuilder.ApplyConfiguration(new RoleToKoboUserConfiguration());
        }

        // DbSets
        public DbSet<Role> Role { get; set; }
        public DbSet<Asset> Asset { get; set; }
        public DbSet<Organization> Organization { get; set; }
        public DbSet<OrganizationProfile> Profile { get; set; }
    }
}