using PG.Models;
using PG.Services.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PG.Services.Contract
{
    public interface IUserService
    {
        /// <summary>
        /// Gets all regular users.
        /// </summary>
        Task<IList<UserDTO>> GetAllRegularUsers();

        /// <summary>
        /// Ban user by ID
        /// </summary>
        /// <param name="id">User's ID</param>
        /// <returns>Returns true if banning was successful</returns>
        Task<bool> BanUserById(string id);

        /// <summary>
        /// Unban user by ID
        /// </summary>
        /// <param name="id">User's ID</param>
        /// <returns>Returns true if unbanning was successful</returns>
        Task<bool> UnbanUserById(string id);

        /// <summary>
        /// Authenticates by username and password.
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <returns>Returns authenticated user</returns>
        User Authenticate(string username, string password);

        /// <summary>
        /// Gets all users
        /// </summary>
        IEnumerable<User> GetAll();
    }
}
