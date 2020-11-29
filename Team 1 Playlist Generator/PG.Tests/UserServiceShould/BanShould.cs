using Microsoft.VisualStudio.TestTools.UnitTesting;
using PG.Models;
using System.Threading.Tasks;

namespace PG.Tests.UserServiceShould
{
    [TestClass]
    public class BanShould
    {
        [TestMethod]
        public async Task BanUser()
        {
            User user = new User { NormalizedUserName = "eto" };

        }
    }
}