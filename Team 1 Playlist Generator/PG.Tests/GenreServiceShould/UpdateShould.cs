using Microsoft.VisualStudio.TestTools.UnitTesting;
using PG.Data.Context;
using PG.Services;
using PG.Services.DTOs;
using PG.Services.Helpers;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PG.Tests.GenreServiceShould
{
    [TestClass]
    public class UpdateShould
    {
        [TestMethod]
        public async Task UpdateCorrectly()
        {
            var options = Utils.GetOptions(nameof(UpdateCorrectly));

            var popGenre = new GenreDTO
            {
                Name = "Pop"
            };

            using (var arrangeContext = new PGDbContext(options))
            {
                var sut = new GenreService(
                    arrangeContext,
                    new ArtistService(arrangeContext),
                    new SongService(arrangeContext),
                    new HttpDeezerClientService(
                        new HttpClient()
                        )
                    );

                await sut.Create(popGenre);

                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new PGDbContext(options))
            {
                var sut = new GenreService(
                    assertContext,
                    new ArtistService(assertContext),
                    new SongService(assertContext),
                    new HttpDeezerClientService(
                        new HttpClient()
                        )
                    );

                var userPalylists = await sut.Update(1, popGenre);

                Assert.AreEqual(popGenre.Name, userPalylists.Name);

            }
        }

        [TestMethod]
        public async Task UpdateThrowsWhenNotFound()
        {
            var options = Utils.GetOptions(nameof(UpdateThrowsWhenNotFound));

            var popGenre = new GenreDTO
            {
                Name = "Pop"
            };
            var assertContext = new PGDbContext(options);

            var sut = new GenreService(
                assertContext,
                new ArtistService(assertContext),
                new SongService(assertContext),
                new HttpDeezerClientService(
                    new HttpClient()
                    )
                );

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.Update(1, popGenre));
        }
    }
}

