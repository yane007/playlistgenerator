using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PG.Data.Context;
using PG.Models;
using PG.Services;
using PG.Services.DTOs;
using System.Linq;
using System.Threading.Tasks;

namespace PG.Tests.PlaylistServiceShould
{
    [TestClass]
    public class GetAllPlaylistsSould
    {
        [TestMethod]
        public async Task GetAllPlaylistsCorrectly()
        {
            var options = Utils.GetOptions(nameof(GetAllPlaylistsCorrectly));
            var pixabayServiceMock = new Mock<IPixabayService>();

            var firstUser = new User
            {
                UserName = "FirstUser",
            };

            var secondUser = new User
            {
                UserName = "SecondUser",
            };

            using (var arrangeContext = new PGDbContext(options))
            {
                var firstUserBd = await arrangeContext.Users.AddAsync(firstUser);
                var secondUserBd = await arrangeContext.Users.AddAsync(secondUser);

                var nirvanaPlaylist = new PlaylistDTO
                {
                    Title = "In Utero",
                    Duration = 1600,
                    UserId = firstUserBd.Entity.Id,
                };

                var acdcPlaylist = new PlaylistDTO
                {
                    Title = "Back in Black",
                    Duration = 2531,
                    UserId = firstUserBd.Entity.Id,
                };

                var scorpionsPLaylist = new PlaylistDTO
                {
                    Title = "Lovedrive",
                    Duration = 2190,
                    UserId = secondUserBd.Entity.Id,
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

                var userPalylists = await sut.GetAllPlaylists();
                int userPalylistsCount = userPalylists.Count();

                Assert.AreEqual(3, userPalylistsCount);
            }
        }

        [TestMethod]
        public async Task GetAllPlaylistsCorrectlyReturnEmpty()
        {
            var options = Utils.GetOptions(nameof(GetAllPlaylistsCorrectlyReturnEmpty));
            var pixabayServiceMock = new Mock<IPixabayService>();
            var assertContext = new PGDbContext(options);

            var sut = new PlaylistService(assertContext, pixabayServiceMock.Object);

            var userPalylists = await sut.GetAllPlaylists();
            int userPalylistsCount = userPalylists.Count();

            Assert.AreEqual(0, userPalylistsCount);
        }
    }
}
