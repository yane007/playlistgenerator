using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PG.Data.Context;
using PG.Services;
using PG.Services.DTOs;
using System;
using System.Threading.Tasks;

namespace PG.Tests.PlaylistServiceShould
{
    [TestClass]
    public class CreateSould
    {
        [TestMethod]
        public async Task CreateCorrectly()
        {
            var options = Utils.GetOptions(nameof(CreateCorrectly));

            var playlist = new PlaylistDTO
            {
                Title = "In Utero",
                Duration = 1600,
                //PixabayImage = "https://en.wikipedia.org/wiki/In_Utero_(album)#/media/File:In_Utero_(Nirvana)_album_cover.jpg",
            };

            using (var arrangeContext = new PGDbContext(options))
            {
                var sut = new PlaylistService(arrangeContext);
                await sut.Create(playlist);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new PGDbContext(options))
            {
                var result = await assertContext.Playlists.FirstOrDefaultAsync(x => x.Title == playlist.Title);

                Assert.AreEqual(playlist.Title, result.Title);
                Assert.AreEqual(playlist.Duration, result.Duration);
                //Assert.AreEqual(playlist.PixabayImage, result.Picture);
            }
        }

        [TestMethod]
        public async Task CreateThrowsWhenNull()
        {
            var options = Utils.GetOptions(nameof(CreateThrowsWhenNull));

            var context = new PGDbContext(options);
            var sut = new PlaylistService(context);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.Create(null));

        }

        [TestMethod]
        public async Task CreateThrowsWhenNameAbove50Chars()
        {
            var options = Utils.GetOptions(nameof(CreateThrowsWhenNameAbove50Chars));


            var context = new PGDbContext(options);
            var sut = new PlaylistService(context);

            var playlist = new PlaylistDTO
            {
                Title = "Lorem ipsum dolor sit amet, consectetur adipiscing elit",
                Duration = 1600,
                //PixabayImage = "https://en.wikipedia.org/wiki/In_Utero_(album)#/media/File:In_Utero_(Nirvana)_album_cover.jpg",
            };

            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => sut.Create(playlist));

        }
    }
}
