using Microsoft.VisualStudio.TestTools.UnitTesting;
using PG.Data.Context;
using PG.Models;
using PG.Services;
using System.Linq;
using System.Threading.Tasks;

namespace PG.Tests.UserServiceShould
{
    [TestClass]
    public class GetAllShould
    {
        [TestMethod]
        public async Task GetAllCorrectly()
        {
            var options = Utils.GetOptions(nameof(GetAllCorrectly));

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
                    IsDeleted = true,
                };

                var thirdUser = new User
                {
                    UserName = "FirstUser",
                    IsDeleted = true,
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

                var allregularUsers = sut.GetAll().ToList();

                Assert.IsTrue(allregularUsers.Count == 4);
            }
        }

        [TestMethod]
        public void GetAllReturnsEmptyWhenNoData()
        {
            var options = Utils.GetOptions(nameof(GetAllReturnsEmptyWhenNoData));

            var assertContext = new PGDbContext(options);

            var sut = new UserService(assertContext);

            var allregularUsers = sut.GetAll().ToList();

            Assert.IsTrue(allregularUsers.Count == 0);
        }
    }
}
