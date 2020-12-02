using Microsoft.VisualStudio.TestTools.UnitTesting;
using PG.Data.Context;
using PG.Models;
using PG.Services;
using System.Linq;
using System.Threading.Tasks;

namespace PG.Tests.UserServiceShould
{
    [TestClass]
    public class UnbanShould
    {
        [TestMethod]
        public async Task UnbanUserByIdCorrectly()
        {
            var options = Utils.GetOptions(nameof(UnbanUserByIdCorrectly));

            string userId = string.Empty;

            using (var arrangeContext = new PGDbContext(options))
            {
                var firstUser = new User
                {
                    UserName = "FirstUser",
                    LockoutEnabled = true,
                };

                var addedUser = arrangeContext.Add(firstUser);

                userId = addedUser.Entity.Id;

                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new PGDbContext(options))
            {
                var sut = new UserService(assertContext);

                await sut.BanUserById(userId);
                await sut.UnbanUserById(userId);
                var bannedUser = assertContext.Users.FirstOrDefault();

                Assert.IsTrue(bannedUser.LockoutEnd == null);
            }
        }

        [TestMethod]
        public async Task UnbanUserByIdReturnsFalseWhenNotFound()
        {
            var options = Utils.GetOptions(nameof(UnbanUserByIdReturnsFalseWhenNotFound));

            var assertContext = new PGDbContext(options);

            var sut = new UserService(assertContext);

            var failedBan = await sut.UnbanUserById("bfd-hdfh7452-bfdbk");

            Assert.IsFalse(failedBan);
        }
    }
}
