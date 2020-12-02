using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PG.Data.Context;
using PG.Models;
using PG.Services;
using PG.Services.DTOs;
using System.Threading.Tasks;

namespace PG.Tests.PlaylistServiceShould
{
    [TestClass]
    public class GetMaxPlaylistDurationShould
    {
        [TestMethod]
        public async Task GetMaxPlaylistDurationCorrectly()
        {
            var options = Utils.GetOptions(nameof(GetMaxPlaylistDurationCorrectly));
            var pixabayServiceMock = new Mock<IPixabayService>();

            var firstUser = new User
            {
                UserName = "FirstUser",
            };

            using (var arrangeContext = new PGDbContext(options))
            {
                var firstUserBd = await arrangeContext.Users.AddAsync(firstUser);

                var firstUserId = firstUserBd.Entity.Id;

                var nirvanaPlaylist = new PlaylistDTO
                {
                    Title = "In Utero",
                    Duration = 1600,
                    UserId = firstUserId,
                };

                var acdcPlaylist = new PlaylistDTO
                {
                    Title = "Back in Black",
                    Duration = 2531,
                    UserId = firstUserId,
                };

                var scorpionsPLaylist = new PlaylistDTO
                {
                    Title = "Lovedrive",
                    Duration = 2190,
                    UserId = firstUserId,
                };

                var sut = new PlaylistService(arrangeContext, pixabayServiceMock.Object);

                await sut.Create(nirvanaPlaylist);
                await sut.Create(acdcPlaylist);
                await sut.Create(scorpionsPLaylist);

                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new PGDbContext(options))
            {
                var sut = new PlaylistService(assertContext, pixabayServiceMock.Object);

                var userPalylists = await sut.GetMaxPlaylistDuration();

                Assert.AreEqual(2531, userPalylists);
            }
        }

        [TestMethod]
        public async Task GetMaxPlaylistDurationReturns0WhenNoPlaylist()
        {
            var options = Utils.GetOptions(nameof(GetMaxPlaylistDurationReturns0WhenNoPlaylist));
            var pixabayServiceMock = new Mock<IPixabayService>();

            var assertContext = new PGDbContext(options);

            var sut = new PlaylistService(assertContext, pixabayServiceMock.Object);

            var userPalylists = await sut.GetMaxPlaylistDuration();

            Assert.AreEqual(0, userPalylists);
        }

        [TestMethod]
        public async Task GetMaxPlaylistDurationReturnDurationOnOnlyPlaylistOnDB()
        {
            var options = Utils.GetOptions(nameof(GetMaxPlaylistDurationReturnDurationOnOnlyPlaylistOnDB));
            var pixabayServiceMock = new Mock<IPixabayService>();

            var firstUser = new User
            {
                UserName = "FirstUser",
            };

            using (var arrangeContext = new PGDbContext(options))
            {
                var firstUserBd = await arrangeContext.Users.AddAsync(firstUser);

                var nirvanaPlaylist = new PlaylistDTO
                {
                    Title = "In Utero",
                    Duration = 1600,
                    UserId = firstUserBd.Entity.Id,
                };

                var sut = new PlaylistService(arrangeContext, pixabayServiceMock.Object);

                await sut.Create(nirvanaPlaylist);

                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new PGDbContext(options))
            {
                var sut = new PlaylistService(assertContext, pixabayServiceMock.Object);

                var userPalylists = await sut.GetMaxPlaylistDuration();

                Assert.AreEqual(1600, userPalylists);
            }
        }
    }
}
