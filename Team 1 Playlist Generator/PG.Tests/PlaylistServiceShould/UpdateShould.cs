using Microsoft.VisualStudio.TestTools.UnitTesting;
using PG.Data.Context;
using PG.Services;
using PG.Services.DTOs;
using PG.Services.Exceptions;
using PG.Services.Helpers;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PG.Tests.PlaylistServiceShould
{
    [TestClass]
    public class UpdateShould
    {
        [TestMethod]
        public async Task UpdateCorrectly()
        {
            var options = Utils.GetOptions(nameof(UpdateCorrectly));

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

                var userPalylists = await sut.Update(1, acdcPlaylist);

                Assert.AreEqual(acdcPlaylist.Title, userPalylists.Title);
            }
        }

        [TestMethod]
        public async Task UpdateThrowsWhenNotFound()
        {
            var options = Utils.GetOptions(nameof(UpdateThrowsWhenNotFound));

            var acdcPlaylist = new PlaylistDTO
            {
                Title = "Back in Black",
                Duration = 2531, 
            };

            var assertContext = new PGDbContext(options);

            var sut = new PlaylistService(
                assertContext,
                new PixabayService(
                  new HttpPixabayClientService(
                      new HttpClient()
                      )
                    )
                );

            await Assert.ThrowsExceptionAsync<NotFoundException>(() => sut.Update(1, acdcPlaylist));
        }
    }
}
