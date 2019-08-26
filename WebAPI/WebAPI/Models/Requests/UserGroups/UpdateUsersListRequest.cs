using System;
using System.Collections.Generic;

namespace WebAPI.Models.Requests.UserGroups
{
    public class UpdateUsersListRequest
    {
        public IEnumerable<Guid> UserIds { get; set; }
    }
}
