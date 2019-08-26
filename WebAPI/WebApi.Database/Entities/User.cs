using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace WebApi.Database.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? UserGroupId { get; set; }

        public ICollection<ApplicationUserRole> UserRoles { get; set; }
        public virtual UserGroup UserGroup { get; set; }
    }
}
