using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.Requests
{
    public class SignInRequest
    {
        public string Email { get; set; }
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
