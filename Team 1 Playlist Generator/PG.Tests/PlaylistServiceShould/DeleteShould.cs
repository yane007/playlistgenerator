using Microsoft.VisualStudio.TestTools.UnitTesting;
using PG.Data.Context;
using PG.Services;
using PG.Services.DTOs;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PG.Tests.PlaylistServiceShould
{
    [TestClass]
    public class DeleteShould
    {
        [TestMethod]
        public async Task DeleteCorrectly()
        {
            var options = Utils.GetOptions(nameof(DeleteCorrectly));

            var playlist = new PlaylistDTO
            {
                Title = "In Utero",
                Duration = 1600,
                Picture = "https://en.wikipedia.org/wiki/In_Utero_(album)#/media/File:In_Utero_(Nirvana)_album_cover.jpg",
            };

            using (var arrangeContext = new PGDbContext(options))
            {
                var sut = new PlaylistService(arrangeContext);
                await sut.Create(playlist);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new PGDbContext(options))
            {
                var sut = new PlaylistService(assertContext);
                await sut.Delete(1);

                var playlists = await sut.GetAllPlaylists();
                int playlistsCount = playlists.Count();

                Assert.AreEqual(0, playlistsCount);
            }
        }

        [TestMethod]
        public async Task DeleteThrowsWhenInvalidId()
        {
            var options = Utils.GetOptions(nameof(DeleteThrowsWhenInvalidId));

            var assertContext = new PGDbContext(options);

            var sut = new PlaylistService(assertContext);

            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => sut.Delete(-1));
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => sut.Delete(0));
        }

        [TestMethod]
        public async Task DeleteThrowsWhenIdDoesNotExist()
        {
            var options = Utils.GetOptions(nameof(DeleteThrowsWhenIdDoesNotExist));

            var playlist = new PlaylistDTO
            {
                Title = "In Utero",
                Duration = 1600,
                Picture = "https://en.wikipedia.org/wiki/In_Utero_(album)#/media/File:In_Utero_(Nirvana)_album_cover.jpg",
            };

            using (var arrangeContext = new PGDbContext(options))
            {
                var sut = new PlaylistService(arrangeContext);
                await sut.Create(playlist);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new PGDbContext(options))
            {
                var sut = new PlaylistService(assertContext);

                await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => sut.Delete(2));
            }

        }

        [TestMethod]
        public async Task DeleteThrowsWhenIdAlreadyDeleted()
        {
            var options = Utils.GetOptions(nameof(DeleteThrowsWhenIdAlreadyDeleted));

            var playlist = new PlaylistDTO
            {
                Title = "In Utero",
                Duration = 1600,
                Picture = "https://en.wikipedia.org/wiki/In_Utero_(album)#/media/File:In_Utero_(Nirvana)_album_cover.jpg",
            };

            using (var arrangeContext = new PGDbContext(options))
            {
                var sut = new PlaylistService(arrangeContext);
                await sut.Create(playlist);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new PGDbContext(options))
            {
                var sut = new PlaylistService(assertContext);

                await sut.Delete(1);

                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.Delete(1));
            }

        }
    }
}
