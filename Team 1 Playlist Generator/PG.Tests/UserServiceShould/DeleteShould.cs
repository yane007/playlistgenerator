using Microsoft.VisualStudio.TestTools.UnitTesting;
using PG.Data.Context;
using PG.Models;
using PG.Services;
using System.Linq;
using System.Threading.Tasks;

namespace PG.Tests.UserServiceShould
{
    [TestClass]
    public class DeleteShould
    {
        [TestMethod]
        public async Task DeleteUserByIdCorrectly()
        {
            var options = Utils.GetOptions(nameof(DeleteUserByIdCorrectly));

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

                await sut.DeleteUserById(userId);
                var deletedUser = assertContext.Users.FirstOrDefault();

                Assert.IsTrue(deletedUser != null);
                Assert.IsTrue(deletedUser.IsDeleted);
            }
        }

        [TestMethod]
        public async Task DeleteUserByIdReturnsWhenNotFound()
        {
            var options = Utils.GetOptions(nameof(DeleteUserByIdReturnsWhenNotFound));

            var assertContext = new PGDbContext(options);

            var sut = new UserService(assertContext);

            await sut.DeleteUserById("nhg-asr045-nhjgffd");

            Assert.IsTrue(true);
        }
    }
}
