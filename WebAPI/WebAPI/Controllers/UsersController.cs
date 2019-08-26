using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi.Database.Entities;
using WebAPI.Attributes;
using WebAPI.Contracts;
using WebAPI.Models.Enums;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly IUsersService _usersService;
        private readonly UserManager<User> _userManager;

        public UsersController(IUsersService usersService, UserManager<User> userManager)
        {
            _usersService = usersService;
            _userManager = userManager;
        }

        [HttpGet]
        [ApplicationAuthorize(UserRoles.ADMIN, UserRoles.MODERATOR)]
        public async Task<IActionResult> GetUsersAsync()
        {
            return Ok(await _usersService.GetApplicationUsersAsync());
        }

        [HttpGet("UserInfo")]
        [ApplicationAuthorize(UserRoles.ADMIN, UserRoles.MODERATOR, UserRoles.USER)]
        public async Task<IActionResult> GetUserAsync()
        {
            var user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            return Ok(await _usersService.GetApplicationUserAsync(user));
        }
    }
}
