using PG.Models;
using PG.Services.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PG.Services.Contract
{
    public interface IUserService
    {
        Task<IList<UserDTO>> GetAllRegularUsers();
        Task<bool> BanUserById(string id);
        Task<bool> UnbanUserById(string id);
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
    }
}
