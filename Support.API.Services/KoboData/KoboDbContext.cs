using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Support.API.Services.KoboData
{
    public class KoboDbContext : DbContext
    {
        public KoboDbContext(DbContextOptions<KoboDbContext> options)
            : base(options)
        {

        }
        public virtual DbSet<KoboUser> KoboUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new KoboDbContextBuilder().BuildModel(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
    }
}
