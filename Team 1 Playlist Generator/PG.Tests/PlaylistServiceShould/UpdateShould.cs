using Microsoft.VisualStudio.TestTools.UnitTesting;
using PG.Data.Context;
using PG.Services;
using PG.Services.DTOs;
using PG.Services.Exceptions;
using System;
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
                PixabayImage = "https://en.wikipedia.org/wiki/In_Utero_(album)#/media/File:In_Utero_(Nirvana)_album_cover.jpg",
            };

            var acdcPlaylist = new PlaylistDTO
            {
                Title = "Back in Black",
                Duration = 2531,
                PixabayImage = "https://upload.wikimedia.org/wikipedia/commons/thumb/9/92/ACDC_Back_in_Black.png/220px-ACDC_Back_in_Black.png",
            };

            using (var arrangeContext = new PGDbContext(options))
            {
                var sut = new PlaylistService(arrangeContext);

                await sut.Create(nirvanaPlaylist);

                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new PGDbContext(options))
            {
                var sut = new PlaylistService(assertContext);

                var userPalylists = await sut.Update(1, acdcPlaylist);

                Assert.AreEqual(acdcPlaylist.Title, userPalylists.Title);
                Assert.AreEqual(acdcPlaylist.PixabayImage, userPalylists.PixabayImage);
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
                PixabayImage = "https://upload.wikimedia.org/wikipedia/commons/thumb/9/92/ACDC_Back_in_Black.png/220px-ACDC_Back_in_Black.png",
            };

            var assertContext = new PGDbContext(options);

            var sut = new PlaylistService(assertContext);

            await Assert.ThrowsExceptionAsync<NotFoundException>(() => sut.Update(1, acdcPlaylist));
        }
    }
}
