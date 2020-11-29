using Microsoft.VisualStudio.TestTools.UnitTesting;
using PG.Data.Context;
using PG.Services;
using PG.Services.DTOs;
using PG.Services.Helpers;
using System.Linq;
using System.Net.Http;
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

            var nirvanaPlaylist = new PlaylistDTO
            {
                Title = "In Utero",
                Duration = 1600,
                UserId = "153a257-526504u",
            };

            var acdcPlaylist = new PlaylistDTO
            {
                Title = "Back in Black",
                Duration = 2531,
                UserId = "153a257-526504u",
            };

            var scorpionsPLaylist = new PlaylistDTO
            {
                Title = "Lovedrive",
                Duration = 2190,
                UserId = "68910y78a-89as1568",
            };

            using (var arrangeContext = new PGDbContext(options))
            {
                var sut = new PlaylistService(
                    arrangeContext,
                    new PixabayService(
                      new HttpPixabayClientService(
                          new HttpClient()
                          )
                        )
                    );

                await sut.Create(nirvanaPlaylist);
                await sut.Create(acdcPlaylist);
                await sut.Create(scorpionsPLaylist);

                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new PGDbContext(options))
            {
                var sut = new PlaylistService(
                    assertContext,
                    new PixabayService(
                      new HttpPixabayClientService(
                          new HttpClient()
                          )
                        )
                    );

                var userPalylists = await sut.GetAllPlaylists();
                int userPalylistsCount = userPalylists.Count();

                Assert.AreEqual(3, userPalylistsCount);
            }
        }

        [TestMethod]
        public async Task GetAllPlaylistsCorrectlyReturnEmpty()
        {
            var options = Utils.GetOptions(nameof(GetAllPlaylistsCorrectlyReturnEmpty));
            var assertContext = new PGDbContext(options);

            var sut = new PlaylistService(
                assertContext,
                new PixabayService(
                  new HttpPixabayClientService(
                      new HttpClient()
                      )
                    )
                );

            var userPalylists = await sut.GetAllPlaylists();
            int userPalylistsCount = userPalylists.Count();

            Assert.AreEqual(0, userPalylistsCount);
        }
    }
}
