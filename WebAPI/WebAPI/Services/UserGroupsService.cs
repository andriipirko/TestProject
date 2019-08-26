using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Database;
using WebApi.Database.Entities;
using WebAPI.Contracts;
using md = WebAPI.Models;

namespace WebAPI.Services
{
    public class UserGroupsService : IUserGroupsService
    {
        private readonly ApplicationDbContext _context;

        public UserGroupsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<md.UserGroup>> GetUserGroupsAsync()
        {
            var result = await _context.UserGroups
                .Include(ug => ug.Users)
                .Select(ug => new md.UserGroup
                {
                    Id = ug.Id,
                    Name = ug.Name,
                    Users = ug.Users.Select(u => new md.User
                    {
                        Id = new Guid(u.Id),
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Email = u.Email
                    })
                })
                .ToListAsync();
            return result;
        }

        public async Task CreateUserGroupAsync(string userGroupName)
        {
            _context.UserGroups.Add(new UserGroup
            {
                Name = userGroupName
            });
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUsersListInUserGroup(int userGroupId, IEnumerable<Guid> userIds)
        {
            var existingUsers = await _context.UserGroups
                .Include(ug => ug.Users)
                .Where(ug => ug.Id == userGroupId)
                .SelectMany(ug => ug.Users)
                .ToListAsync();
            //var existingUsers = await _context.Users
            //    .Where(u => userIds.Contains(new Guid(u.Id)) && u.UserGroupId == userGroupId)
            //    .ToListAsync();

            var userIdsToAdd = userIds.Where(i => !existingUsers.Select(eu => new Guid(eu.Id)).Contains(i));
            var userIdsToRemove = userIds.Except(userIdsToAdd);

            var users = await _context.Users
                .Where(u => userIds.Contains(new Guid(u.Id)))
                .ToListAsync();

            users.Where(u => userIdsToAdd.Contains(new Guid(u.Id))).ToList().ForEach(u => u.UserGroupId = userGroupId);
            users.Where(u => userIdsToRemove.Contains(new Guid(u.Id))).ToList().ForEach(u => u.UserGroupId = null);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveUserGroupByIdAsync(int userGroupId)
        {
            var userGroup = await _context.UserGroups.FirstOrDefaultAsync(ug => ug.Id == userGroupId);
            _context.UserGroups.Remove(userGroup);
            await _context.SaveChangesAsync();
        }
    }
}
