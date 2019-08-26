using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebAPI.Contracts;
using WebAPI.Models.Requests;

namespace WebAPI.Controllers
{
    [Route("[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignInAsync([FromBody] SignInRequest request)
        {
            return Ok(await _authenticationService.SignInAsync(request));
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUpAsync([FromBody] SignUpRequest request)
        {
            return Ok(await _authenticationService.SignUpAsync(request));
        }
    }
}
