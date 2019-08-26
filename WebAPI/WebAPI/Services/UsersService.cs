using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Database;
using WebAPI.Contracts;
using WebAPI.Models;
using db = WebApi.Database.Entities;

namespace WebAPI.Services
{
    public class UsersService : IUsersService
    {
        private readonly ApplicationDbContext _context;

        public UsersService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetApplicationUsersAsync()
        {
            var result = await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .Select(u => new User
                {
                    Id = new Guid(u.Id),
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    Role = u.UserRoles.First().Role.Name
                }).ToListAsync();
            return result;
        }

        public async Task<User> GetApplicationUserAsync(db.User user)
        {
            return await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .Select(u => new User
                {
                    Id = new Guid(u.Id),
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    Role = u.UserRoles.First().Role.Name
                }).FirstOrDefaultAsync();
        }
    }
}
