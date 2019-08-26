using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Database.Entities;
using md = WebAPI.Models;

namespace WebAPI.Contracts
{
    public interface IUserGroupsService
    {
        Task<IEnumerable<md.UserGroup>> GetUserGroupsAsync();
        Task CreateUserGroupAsync(string userGroupName);
        Task UpdateUsersListInUserGroup(int userGroupId, IEnumerable<Guid> userIds);
        Task RemoveUserGroupByIdAsync(int userGroupId);
    }
}
