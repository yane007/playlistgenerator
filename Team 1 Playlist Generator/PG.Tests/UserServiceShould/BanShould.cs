using Microsoft.VisualStudio.TestTools.UnitTesting;
using PG.Data.Context;
using PG.Models;
using PG.Services;
using System.Threading.Tasks;

namespace PG.Tests.UserServiceShould
{
    [TestClass]
    public class BanShould
    {
        [TestMethod]
        public async Task CorrectlyBanUser()
        {
            var options = Utils.GetOptions(nameof(CorrectlyBanUser));

            var actContext = new PGDbContext(options);

            User user = new User { NormalizedUserName = "Georgi" };
            actContext.Add(user);
            await actContext.SaveChangesAsync();

            var sut = new UserService(actContext);
            await sut.BanUserById(user.Id);

            Assert.IsTrue(user.LockoutEnabled);
        }
    }
}