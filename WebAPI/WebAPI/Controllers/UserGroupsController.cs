using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using WebAPI.Attributes;
using WebAPI.Contracts;
using WebAPI.Models.Enums;
using WebAPI.Models.Requests.UserGroups;

namespace WebAPI.Controllers
{
    [Route("[controller]")]
    public class UserGroupsController : Controller
    {
        private readonly IUserGroupsService _userGroupsService;

        public UserGroupsController(IUserGroupsService userGroupsService)
        {
            _userGroupsService = userGroupsService;
        }

        [HttpGet]
        [ApplicationAuthorize(UserRoles.ADMIN, UserRoles.MODERATOR)]
        public async Task<IActionResult> GetUserGroupsAsync()
        {
            return Ok(await _userGroupsService.GetUserGroupsAsync());
        }

        [HttpPost]
        [ApplicationAuthorize(UserRoles.ADMIN)]
        public async Task<IActionResult> CreateUserGroupAsync([FromBody] UserGroupCreateRequest request)
        {
            await _userGroupsService.CreateUserGroupAsync(request.UserGroupName);
            return new StatusCodeResult((int)HttpStatusCode.Created);
        }

        [HttpPut("{userGroupId}")]
        [ApplicationAuthorize(UserRoles.ADMIN)]
        public async Task<IActionResult> UpdateUserGroupListAsync(int userGroupId, [FromBody] UpdateUsersListRequest request)
        {
            await _userGroupsService.UpdateUsersListInUserGroup(userGroupId, request.UserIds);
            return NoContent();
        }

        [HttpDelete("{userGroupId}")]
        [ApplicationAuthorize(UserRoles.ADMIN)]
        public async Task<IActionResult> DeleteUserGroupAsync(int userGroupId)
        {
            await _userGroupsService.RemoveUserGroupByIdAsync(userGroupId);
            return Ok();
        }
    }
}
