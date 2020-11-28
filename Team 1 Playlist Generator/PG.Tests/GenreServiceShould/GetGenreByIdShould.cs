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
    public class GetGenreByIdShould
    {
        [TestMethod]
        public async Task GetGenreByIdCorrectly()
        {
            var options = Utils.GetOptions(nameof(GetGenreByIdCorrectly));

            var rockGenre = new GenreDTO
            {
                Name = "Rock"
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

                await sut.Create(rockGenre);

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

                var userPalylists = await sut.GetGenreById(1);

                Assert.AreEqual(rockGenre.Name, userPalylists.Name);
            }
        }

        [TestMethod]
        public async Task ThrownsWhenGenreIdIsNotFound()
        {
            var options = Utils.GetOptions(nameof(ThrownsWhenGenreIdIsNotFound));

            var assertContext = new PGDbContext(options);

            var sut = new GenreService(
                assertContext,
                new ArtistService(assertContext),
                new SongService(assertContext),
                new HttpDeezerClientService(
                    new HttpClient()
                    )
                );

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.GetGenreById(1));
        }
    }
}