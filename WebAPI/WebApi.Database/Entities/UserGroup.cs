using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace WebApi.Database.Entities
{
    public class UserGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<User> Users { get; set; }

        public static void SetupKeys(ModelBuilder builder)
        {
            var entity = builder.Entity<UserGroup>();
            entity.HasKey(e => e.Id);

            entity.HasMany(e => e.Users)
                .WithOne(u => u.UserGroup);
        }
    }
}
