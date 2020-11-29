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

namespace PG.Tests.GenreServiceShould
{
    [TestClass]
    public class DeleteShould
    {
        [TestMethod]
        public async Task DeleteCorrectly()
        {
            var options = Utils.GetOptions(nameof(DeleteCorrectly));

            var genre = new GenreDTO
            {
                Name = "Jazz"
            };

            using (var arrangeContext = new PGDbContext(options))
            {
                var sut = new GenreService(
                    arrangeContext,
                    new ArtistService(arrangeContext),
                    new SongService(arrangeContext),
                    new HttpDeezerClientService(new HttpClient())
                    );

                await sut.Create(genre);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new PGDbContext(options))
            {
                var sut = new GenreService(
                    assertContext,
                    new ArtistService(assertContext),
                    new SongService(assertContext),
                    new HttpDeezerClientService(new HttpClient())
                    );

                await sut.Delete(1);
                var genres = await sut.GetAllGenres();
                int genresCount = genres.Count();

                Assert.AreEqual(0, genresCount);
            }
        }

        [TestMethod]
        public async Task DeleteThrowsWhenInvalidId()
        {
            var options = Utils.GetOptions(nameof(DeleteThrowsWhenInvalidId));

            var assertContext = new PGDbContext(options);

            var sut = new GenreService(
                assertContext,
                new ArtistService(assertContext),
                new SongService(assertContext),
                new HttpDeezerClientService(new HttpClient())
                );

            await Assert.ThrowsExceptionAsync<NotFoundException>(() => sut.Delete(-1));
        }

        [TestMethod]
        public async Task DeleteThrowsWhenIdDoesNotExist()
        {
            var options = Utils.GetOptions(nameof(DeleteThrowsWhenIdDoesNotExist));

            using (var assertContext = new PGDbContext(options))
            {
                var sut = new GenreService(
                    assertContext,
                    new ArtistService(assertContext),
                    new SongService(assertContext),
                    new HttpDeezerClientService(new HttpClient())
                    );

                await Assert.ThrowsExceptionAsync<NotFoundException>(() => sut.Delete(2));
            }
        }

        [TestMethod]
        public async Task DeleteThrowsWhenIdAlreadyDeleted()
        {
            var options = Utils.GetOptions(nameof(DeleteThrowsWhenIdAlreadyDeleted));

            var genre = new GenreDTO
            {
                Name = "Jazz"
            };

            using (var arrangeContext = new PGDbContext(options))
            {
                var sut = new GenreService(
                    arrangeContext,
                    new ArtistService(arrangeContext),
                    new SongService(arrangeContext),
                    new HttpDeezerClientService(new HttpClient())
                    );

                await sut.Create(genre);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new PGDbContext(options))
            {
                var sut = new GenreService(
                    assertContext,
                    new ArtistService(assertContext),
                    new SongService(assertContext),
                    new HttpDeezerClientService(new HttpClient())
                    );

                await sut.Delete(1);

                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.Delete(1));
            }
        }
    }
}
