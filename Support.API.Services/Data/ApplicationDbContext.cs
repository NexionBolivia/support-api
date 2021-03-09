using Microsoft.EntityFrameworkCore;
using Support.API.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Support.API.Services.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new ApplicationDbContextBuilder().BuildModel(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        // DbSets
        public DbSet<Role> Roles { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<OrganizationProfile> OrganizationProfiles { get; set; }

        public DbSet<RoleToKoboUser> RolesToKoboUsers { get; set; }
        public DbSet<OrganizationToKoboUser> OrganizationsToKoboUsers { get; set; }
        public DbSet<RoleToAsset> RoleToAssets { get; set; }
    }
}