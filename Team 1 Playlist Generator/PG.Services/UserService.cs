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

        public UserService(PGDbContext context)
        {
            _context = context;
        }


        public async Task<IList<UserDTO>> GetAllRegularUsers()
        {
            return await _context.Users.Where(x => x.IsDeleted == false)
                                        .Select(x => x.ToDTO())
                                        .ToListAsync();
        }

        public async Task<bool> BanUserById(string id)
        {
            var userToBan = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (userToBan == null)
            {
                return false;
            }

            if (userToBan.LockoutEnabled == false)
            {
                userToBan.LockoutEnabled = true;

                userToBan.LockoutEnd = new DateTimeOffset(DateTime.Today).AddDays(5);

                await _context.SaveChangesAsync();
            }

            return true;
        }

        public async Task<bool> UnbanUserById(string id)
        {
            var userToBan = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (userToBan == null)
            {
                return false;
            }

            if (userToBan.LockoutEnabled == true)
            {
                userToBan.LockoutEnabled = false;
                await _context.SaveChangesAsync();
            }

            return true;
        }

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
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user;
        }


        public IEnumerable<User> GetAll()
        {
            return this._context.Users;
        }
    }
}
