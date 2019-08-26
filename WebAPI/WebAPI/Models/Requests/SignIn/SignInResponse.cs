using System;

namespace WebAPI.Models.Requests
{
    public class SignInResponse
    {
        public string Token { get; set; }
        public Guid UserId { get; set; }
        public string Role { get; set; }
    }
}
