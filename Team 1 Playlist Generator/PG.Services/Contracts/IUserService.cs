using PG.Models;
using System.Collections.Generic;

namespace PG.Services.Contract
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
    }
}
