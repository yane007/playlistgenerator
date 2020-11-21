using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PG.Data.Context;
using PG.Models;
using PG.Services.Contract;
using PG.Services.DTOs;
using PG.Services.Helpers;
using PG.Services.Mappers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PG.Services
{
    public class UserService : IUserService
    {
        private readonly PGDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly AppSettings _appSettings;

        public UserService(PGDbContext context, UserManager<User> userManager, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _userManager = userManager;
            _appSettings = appSettings.Value;
        }
        /// <summary>
        /// This method takes all regular users from database
        /// </summary>
        /// <returns>Return all regular users</returns>
        public async Task<IList<UserDTO>> GetAllRegularUsers()
        {
            return await _context.Users.Where(x => x.IsDeleted == false)
                                        .Select(x => x.ToDTO())
                                        .ToListAsync();
        }
        //public async Task<IList<UserDTO>> GetAllRegularUsers()
        //{
        //    var regularUsers = new List<UserDTO>();
        //    var users = this._context.Users.Select(x => x.ToDTO()).ToList();

        //    foreach (var u in users)
        //    {
        //        if (!await this._userManager.IsInRoleAsync(u, "Admin"))
        //        {
        //            regularUsers.Add(u);
        //        }
        //    }

        //    return regularUsers;
        //}
        /// <summary>
        /// This method ban user by Id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Returns true if user is banned</returns>
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
        /// <summary>
        /// This method unban user by it's id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Returns true if user's ban is removed succesfully</returns>
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
        /// <summary>
        /// This method authenticate the user by it's username and password
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <returns>Returns authenticated user</returns>
        public User Authenticate(string username, string password)
        {

            var user = _context.Users.SingleOrDefault(x => x.UserName == username);

            var verifyResult = _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            if (verifyResult != PasswordVerificationResult.Success)
            {
                return null;
            }


            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user;
        }
        /// <summary>
        /// This method is from all users in database
        /// </summary>
        /// <returns>Returns all users from database</returns>
        public IEnumerable<User> GetAll()
        {
            return this._context.Users;
        }
    }
}
