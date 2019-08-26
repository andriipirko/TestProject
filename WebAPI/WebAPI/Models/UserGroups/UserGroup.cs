using System.Collections.Generic;

namespace WebAPI.Models
{
    public class UserGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<User> Users { get; set; }
    }
}
