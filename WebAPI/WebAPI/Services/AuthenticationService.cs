using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApi.Database;
using WebApi.Database.Entities;
using WebAPI.Contracts;
using WebAPI.Exceptions;
using WebAPI.Models.Enums;
using WebAPI.Models.Requests;

namespace WebAPI.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly AuthenticationOptions _authenticationOptions;
        private readonly ApplicationDbContext _context;

        public AuthenticationService(UserManager<User> userManager, AuthenticationOptions authenticationOptions, ApplicationDbContext context)
        {
            _userManager = userManager;
            _authenticationOptions = authenticationOptions;
            _context = context;
        }

        public async Task<SignInResponse> SignInAsync(SignInRequest request)
        {
            User user = null;

            if (string.IsNullOrEmpty(request.Email))
                user = await _userManager.FindByNameAsync(request.Login);
            else
                user = await _userManager.FindByEmailAsync(request.Email);

            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName)
            };
            claims.AddRange(roles.Select(role => new Claim(ClaimsIdentity.DefaultRoleClaimType, role)));

            var claimsIdentity = new ClaimsIdentity(claims);

            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                issuer: _authenticationOptions.Issuer,
                audience: _authenticationOptions.Audience,
                notBefore: now,
                claims: claimsIdentity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(_authenticationOptions.LifeTime)),
                signingCredentials: new SigningCredentials(_authenticationOptions.SecurityKey, SecurityAlgorithms.HmacSha256));

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.WriteToken(jwt);

            user = await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == user.Id);

            var response = new SignInResponse
            {
                UserId = new Guid(user.Id),
                Token = token,
                Role = user.UserRoles.First().Role.Name
            };

            return response;
        }

        public async Task<SignUpResponse> SignUpAsync(SignUpRequest request)
        {
            if (!await _context.Roles.AnyAsync())
            {
                _context.Roles.AddRange(new List<ApplicationRole>
                {
                    new ApplicationRole
                    {
                        Name = UserRoles.ADMIN,
                        NormalizedName = UserRoles.ADMIN.ToUpper()
                    },
                    new ApplicationRole
                    {
                        Name = UserRoles.MODERATOR,
                        NormalizedName = UserRoles.MODERATOR.ToUpper()
                    },
                    new ApplicationRole
                    {
                        Name = UserRoles.USER,
                        NormalizedName = UserRoles.USER.ToUpper()
                    }
                });
                await _context.SaveChangesAsync();
            }

            var user = new User
            {
                UserName = request.Login,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName
            };
            var userRegistrationResult = await _userManager.CreateAsync(user, request.Password);

            if (!userRegistrationResult.Succeeded)
                throw new RequestException(userRegistrationResult.Errors
                    .Select(e => e.Description)
                    .First());

            var createdUser = await _userManager.FindByEmailAsync(request.Email);
            var role = string.IsNullOrEmpty(request.UserRole) ? UserRoles.USER : request.UserRole;
            await _userManager.AddToRoleAsync(createdUser, role);
            return new SignUpResponse
            {
                UserId = new Guid(createdUser.Id)
            };
        }
    }
}
