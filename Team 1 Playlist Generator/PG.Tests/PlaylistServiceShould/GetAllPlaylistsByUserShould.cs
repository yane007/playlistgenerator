using Microsoft.VisualStudio.TestTools.UnitTesting;
using PG.Data.Context;
using PG.Models;
using PG.Services;
using PG.Services.DTOs;
using PG.Services.Helpers;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PG.Tests.PlaylistServiceShould
{
    [TestClass]
    public class GetAllPlaylistsByUserShould
    {
        [TestMethod]
        public async Task GetAllPlaylistsByUserCorrectly()
        {
            var options = Utils.GetOptions(nameof(GetAllPlaylistsByUserCorrectly));

            var firstUser = new User
            {
                UserName = "FirstUser",
            };

            var secondUser = new User
            {
                UserName = "SecondUser",
            };

            string firstUserId = string.Empty;
            string secondUserId = string.Empty;

            using (var arrangeContext = new PGDbContext(options))
            {
                var firstUserBd = await arrangeContext.Users.AddAsync(firstUser);
                var secondUserBd = await arrangeContext.Users.AddAsync(secondUser);

                firstUserId = firstUserBd.Entity.Id;
                secondUserId = secondUserBd.Entity.Id;

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
                    UserId = secondUserId,
                };


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
                var playlists = assertContext.Playlists.ToList();

                var firstUserPalylists = await sut.GetPlaylistsByUser(firstUserId);
                var secondUserPalylists = await sut.GetPlaylistsByUser(secondUserId);

                int firstUserPalylistsCount = firstUserPalylists.Count();
                int secondUserPalylistsCount = secondUserPalylists.Count();

                Assert.AreEqual(2, firstUserPalylistsCount);
                Assert.AreEqual(1, secondUserPalylistsCount);
            }
        }

        [TestMethod]
        public async Task GetAllPlaylistsByUserCorrectlyReturnEmpty()
        {
            var options = Utils.GetOptions(nameof(GetAllPlaylistsByUserCorrectlyReturnEmpty));
            var assertContext = new PGDbContext(options);

            var sut = new PlaylistService(
                assertContext,
                new PixabayService(
                  new HttpPixabayClientService(
                      new HttpClient()
                      )
                    )
                );

            var userPalylists = await sut.GetPlaylistsByUser("153a257-526504u");
            int userPalylistsCount = userPalylists.Count();

            Assert.AreEqual(0, userPalylistsCount);
        }
    }
}
