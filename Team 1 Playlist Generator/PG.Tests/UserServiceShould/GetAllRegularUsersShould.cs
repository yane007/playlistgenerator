using Microsoft.VisualStudio.TestTools.UnitTesting;
using PG.Data.Context;
using PG.Models;
using PG.Services;
using System.Linq;
using System.Threading.Tasks;

namespace PG.Tests.UserServiceShould
{
    [TestClass]
    public class GetAllRegularUsersShould
    {
        [TestMethod]
        public async Task GetAllRegularUsersCorrectly()
        {
            var options = Utils.GetOptions(nameof(GetAllRegularUsersCorrectly));

            using (var arrangeContext = new PGDbContext(options))
            {
                var firstUser = new User
                {
                    UserName = "FirstUser",
                    IsDeleted = false,
                };

                var secondUser = new User
                {
                    UserName = "FirstUser",
                    IsDeleted = false,
                };

                var thirdUser = new User
                {
                    UserName = "FirstUser",
                    IsDeleted = false,
                };

                var fourthUser = new User
                {
                    UserName = "FirstUser",
                    IsDeleted = false,
                };

                arrangeContext.Add(firstUser);
                arrangeContext.Add(secondUser);
                arrangeContext.Add(thirdUser);
                arrangeContext.Add(fourthUser);

                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new PGDbContext(options))
            {
                var sut = new UserService(assertContext);

                var allregularUsers = await sut.GetAllRegularUsers();

                Assert.IsTrue(allregularUsers.Count == 4);
            }
        }

        [TestMethod]
        public async Task GetAllRegularUsersReturnsEmptyWhenNoData()
        {
            var options = Utils.GetOptions(nameof(GetAllRegularUsersReturnsEmptyWhenNoData));

            var assertContext = new PGDbContext(options);

            var sut = new UserService(assertContext);

            var allregularUsers = await sut.GetAllRegularUsers();

            Assert.IsTrue(allregularUsers.Count == 0);
        }
    }
}
