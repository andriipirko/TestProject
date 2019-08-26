using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Models;
using db = WebApi.Database.Entities;

namespace WebAPI.Contracts
{
    public interface IUsersService
    {
        Task<IEnumerable<User>> GetApplicationUsersAsync();
        Task<User> GetApplicationUserAsync(db.User user);
    }
}
