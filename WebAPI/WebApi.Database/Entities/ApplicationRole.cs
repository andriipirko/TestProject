using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace WebApi.Database.Entities
{
    public class ApplicationRole : IdentityRole
    {
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
