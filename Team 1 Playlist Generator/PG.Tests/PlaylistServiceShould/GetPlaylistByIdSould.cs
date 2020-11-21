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
                Picture = "https://en.wikipedia.org/wiki/In_Utero_(album)#/media/File:In_Utero_(Nirvana)_album_cover.jpg",
            };

            var acdcPlaylist = new PlaylistDTO
            {
                Title = "Back in Black",
                Duration = 2531,
                Picture = "https://upload.wikimedia.org/wikipedia/commons/thumb/9/92/ACDC_Back_in_Black.png/220px-ACDC_Back_in_Black.png",
            };

            using (var arrangeContext = new PGDbContext(options))
            {
                var sut = new PlaylistService(arrangeContext);

                await sut.Create(nirvanaPlaylist);
                await sut.Create(acdcPlaylist);

                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new PGDbContext(options))
            {
                var sut = new PlaylistService(assertContext);

                var userPalylists = await sut.GetPlaylistById(1);

                Assert.AreEqual(nirvanaPlaylist.Title, userPalylists.Title);
                Assert.AreEqual(nirvanaPlaylist.Duration, userPalylists.Duration);
                Assert.AreEqual(nirvanaPlaylist.Picture, userPalylists.Picture);
            }
        }

        [TestMethod]
        public async Task GetPlaylistByIdThrowsWhenNotFound()//TODO: Може ли да се тества съобщението на exception?
        {
            var options = Utils.GetOptions(nameof(GetPlaylistByIdThrowsWhenNotFound));

            var assertContext = new PGDbContext(options);

            var sut = new PlaylistService(assertContext);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.GetPlaylistById(1));
        }
    }
}
