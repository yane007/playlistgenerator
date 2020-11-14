using Microsoft.AspNetCore.Http;
using PG.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PG.Services
{
    public class UserService
    {
        private readonly PGDbContext _context;

        public UserService(PGDbContext context)
        {
            this._context = context;
        }

        //public async Task<int> GetUserIdAsync(HttpContext http)
        //{
        //    var user = await _context.Users.Find(http.User);

        //    return user.Id;
        //}
    }
}
