using Microsoft.EntityFrameworkCore;
using SupportAPI.Services.Models;
using SupportAPI.Services.Models.ModelConfigurations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupportAPI.Services.Data
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
            modelBuilder.ApplyConfiguration(new OrganizationConfiguration());
            modelBuilder.ApplyConfiguration(new ProfileConfiguration());
            modelBuilder.ApplyConfiguration(new SupportApiUserConfiguration());
            modelBuilder.ApplyConfiguration(new UserKoboConfiguration());
            modelBuilder.ApplyConfiguration(new SupportApiUser_UserKoboConfiguration());
        }

        // DbSets
        public DbSet<SupportApiUser> SupportApiUser { get; set; }
        public DbSet<UserKobo> UserKobo { get; set; }
        public DbSet<Organization> Organization { get; set; }
        public DbSet<Profile> Profile { get; set; }
    }
}