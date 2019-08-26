using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace WebApi.Database.Entities
{
    public class ApplicationUserRole : IdentityUserRole<string>
    {
        public virtual User User { get; set; }
        public virtual ApplicationRole Role { get; set; }

        public static void SetupKeys(ModelBuilder builder)
        {
            var entity = builder.Entity<ApplicationUserRole>();
            entity.HasKey(e => new { e.UserId, e.RoleId });

            entity.HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            entity.HasOne(ur => ur.User)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
        }
    }
}
