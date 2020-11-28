using Microsoft.VisualStudio.TestTools.UnitTesting;
using PG.Data.Context;
using PG.Services;
using PG.Services.DTOs;
using PG.Services.Helpers;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PG.Tests.GenreServiceShould
{
    [TestClass]
    public class GetAllGenresShould
    {
        [TestMethod]
        public async Task GetAllGenresCorrectly()
        {
            var options = Utils.GetOptions(nameof(GetAllGenresCorrectly));

            var popGenre = new GenreDTO
            {
                Name = "Pop"
            };

            var rockGenre = new GenreDTO
            {
                Name = "Rock"
            };

            var metalGenre = new GenreDTO
            {
                Name = "Metal"
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
                await sut.Create(rockGenre);
                await sut.Create(metalGenre);

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

                var userPalylists = await sut.GetAllGenres();
                int userPalylistsCount = userPalylists.Count();

                Assert.AreEqual(3, userPalylistsCount);
            }
        }

        [TestMethod]
        public async Task GetAllGenresCorrectlyReturnEmpty()
        {
            var options = Utils.GetOptions(nameof(GetAllGenresCorrectlyReturnEmpty));

            var assertContext = new PGDbContext(options);

            var sut = new GenreService(
                assertContext,
                new ArtistService(assertContext),
                new SongService(assertContext),
                new HttpDeezerClientService(
                    new HttpClient()
                    )
                );

            var userPalylists = await sut.GetAllGenres();
            int userPalylistsCount = userPalylists.Count();

            Assert.AreEqual(0, userPalylistsCount);
        }
    }
}

