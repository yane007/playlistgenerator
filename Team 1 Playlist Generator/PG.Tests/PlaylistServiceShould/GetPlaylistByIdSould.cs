using Microsoft.VisualStudio.TestTools.UnitTesting;
using PG.Data.Context;
using PG.Services;
using PG.Services.DTOs;
using PG.Services.Helpers;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PG.Tests.PlaylistServiceShould
{
    [TestClass]
    public class GetPlaylistByIdSould
    {

        [TestMethod]
        public async Task GetPlaylistByIdCorrectly()
        {
            var options = Utils.GetOptions(nameof(GetPlaylistByIdCorrectly));

            var nirvanaPlaylist = new PlaylistDTO
            {
                Title = "In Utero",
                Duration = 1600,
            };

            var acdcPlaylist = new PlaylistDTO
            {
                Title = "Back in Black",
                Duration = 2531,
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

                var userPalylists = await sut.GetPlaylistById(1);

                Assert.AreEqual(nirvanaPlaylist.Title, userPalylists.Title);
                Assert.AreEqual(nirvanaPlaylist.Duration, userPalylists.Duration);
                Assert.IsNotNull(userPalylists.PixabayImage);
            }
        }

        [TestMethod]
        public async Task GetPlaylistByIdThrowsWhenNotFound()
        {
            var options = Utils.GetOptions(nameof(GetPlaylistByIdThrowsWhenNotFound));

            var assertContext = new PGDbContext(options);

            var sut = new PlaylistService(
                assertContext,
                new PixabayService(
                  new HttpPixabayClientService(
                      new HttpClient()
                      )
                    )
                );

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.GetPlaylistById(1));
        }
    }
}
