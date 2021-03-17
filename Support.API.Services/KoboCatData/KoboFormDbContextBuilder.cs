using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Support.API.Services.KoboCatData
{
    internal class KoboFormDbContextBuilder
    {
        public virtual void BuildModel(ModelBuilder modelBuilder)
        {
            MapAuthUser(modelBuilder.Entity<KoboCatUser>());
            MapKoboCatAuthToken(modelBuilder.Entity<KoboCatAuthToken>());

        }

        private void MapAuthUser(EntityTypeBuilder<KoboCatUser> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("auth_user");
            entityTypeBuilder.HasKey(p => p.Id);
            entityTypeBuilder.Property(p => p.Id).HasColumnName("id").IsRequired();
            entityTypeBuilder.Property(p => p.LastLogin).HasColumnName("last_login");
            entityTypeBuilder.Property(p => p.IsSuperUser).HasColumnName("is_superuser").IsRequired();
            entityTypeBuilder.Property(p => p.UserName).HasColumnName("username").IsRequired();
            entityTypeBuilder.Property(p => p.FirstName).HasColumnName("first_name").IsRequired();
            entityTypeBuilder.Property(p => p.LastName).HasColumnName("last_name").IsRequired();
            entityTypeBuilder.Property(p => p.Email).HasColumnName("email").IsRequired();
            entityTypeBuilder.Property(p => p.IsStaff).HasColumnName("is_staff").IsRequired();
            entityTypeBuilder.Property(p => p.IsActive).HasColumnName("is_active").IsRequired();
            entityTypeBuilder.Property(p => p.DateJoined).HasColumnName("date_joined").IsRequired();
        }

        private void MapKoboCatAuthToken(EntityTypeBuilder<KoboCatAuthToken> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("authtoken_token");
            entityTypeBuilder.HasKey(p => p.Key);
            entityTypeBuilder.Property(p => p.Key).HasColumnName("key").IsRequired();
            entityTypeBuilder.Property(p => p.Created).HasColumnName("created").IsRequired();
            entityTypeBuilder.Property(p => p.UserId).HasColumnName("user_id").IsRequired();
        }
    }
}
