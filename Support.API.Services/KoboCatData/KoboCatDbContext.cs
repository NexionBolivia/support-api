using Microsoft.EntityFrameworkCore;

namespace Support.API.Services.KoboCatData
{
    public class KoboCatDbContext : DbContext
    {
        public KoboCatDbContext(DbContextOptions<KoboCatDbContext> options)
            : base(options)
        {

        }
        public virtual DbSet<KoboCatAuthToken> AuthTokens { get; set; }

        public virtual DbSet<KoboCatUser> KoboCatUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new KoboFormDbContextBuilder().BuildModel(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
    }
}
