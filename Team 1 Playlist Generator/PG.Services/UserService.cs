using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PG.Data.Context;
using PG.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PG.Services
{
    public class UserService
    {
        private readonly PGDbContext _context;
        private readonly UserManager<User> _userManager;

        public UserService(PGDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IList<User>> GetAllRegularUsers()
        {
            var regularUsers = new List<User>();
            var users = this._context.Users.ToList();

            foreach (var u in users)
            {
                if (!await this._userManager.IsInRoleAsync(u, "Admin"))
                {
                    regularUsers.Add(u);
                }
            }

            return regularUsers;
        }

        public async Task<bool> BanUserById(string id)
        {
            var userToBan = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (userToBan == null)
            {
                return false;
            }

            userToBan.LockoutEnabled = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UnbanUserById(string id)
        {
            var userToBan = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (userToBan == null)
            {
                return false;
            }

            userToBan.LockoutEnabled = false;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
