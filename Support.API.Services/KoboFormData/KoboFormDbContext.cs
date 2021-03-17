using Microsoft.EntityFrameworkCore;

namespace Support.API.Services.KoboFormData
{
    public class KoboFormDbContext : DbContext
    {
        public KoboFormDbContext(DbContextOptions<KoboFormDbContext> options)
            : base(options)
        {

        }
        public virtual DbSet<KoboUser> KoboUsers { get; set; }
        public virtual DbSet<KoboFormAuthToken> AuthTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new KoboFormDbContextBuilder().BuildModel(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
    }
}
