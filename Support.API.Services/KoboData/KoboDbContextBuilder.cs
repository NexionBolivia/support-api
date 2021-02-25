using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Support.API.Services.KoboData
{
    internal class KoboDbContextBuilder
    {
        public virtual void BuildModel(ModelBuilder modelBuilder)
        {
            MapSyncRequest(modelBuilder.Entity<KoboUser>());

        }

        private void MapSyncRequest(EntityTypeBuilder<KoboUser> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("auth_user");
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
    }
}
