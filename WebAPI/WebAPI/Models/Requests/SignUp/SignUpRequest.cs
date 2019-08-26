using System.ComponentModel.DataAnnotations;
using WebAPI.Models.Enums;

namespace WebAPI.Models.Requests
{
    public class SignUpRequest
    {
        [Required]
        public string Login { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserRole { get; set; }
    }
}
