using Microsoft.VisualStudio.TestTools.UnitTesting;
using PG.Data.Context;
using PG.Models;
using PG.Services;
using System.Threading.Tasks;

namespace PG.Tests.UserServiceShould
{
    [TestClass]
    public class GetAllUsersShould
    {
        [TestMethod]
        public async Task CorrectlyTakesAllUsers()
        {
            var options = Utils.GetOptions(nameof(CorrectlyTakesAllUsers));

            var assertContext = new PGDbContext(options);

            User user = new User { NormalizedUserName = "Georgi" };
            User userTwo = new User { NormalizedUserName = "Yane" };
            assertContext.Add(user);
            assertContext.Add(userTwo);
            await assertContext.SaveChangesAsync();

            var sut = new UserService(assertContext);
            var users = await sut.GetAllRegularUsers();

            Assert.AreEqual(2, users.Count);
        }
    }
}