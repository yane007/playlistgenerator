using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PG.Data.Context;
using PG.Services;
using PG.Services.DTOs;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PG.Tests.PlaylistServiceShould
{
    [TestClass]
    public class CreateSould
    {

        [TestMethod]
        public async Task CreateCorrectly()
        {
            var options = Utils.GetOptions(new Guid().ToString());

            var playlist = new PlaylistDTO
            {
                Title = "In Utero",
                Duration = 1600,
                Picture = "https://en.wikipedia.org/wiki/In_Utero_(album)#/media/File:In_Utero_(Nirvana)_album_cover.jpg",
            };

            using (var arrangeContext = new PGDbContext(options))
            {
                var sut = new PlaylistService(arrangeContext);
                var review = await sut.Create(playlist);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new PGDbContext(options))
            {
                var result = await assertContext.Playlists.FirstOrDefaultAsync(x => x.Title == playlist.Title);

                Assert.AreEqual(playlist.Title, result.Title);
                Assert.AreEqual(playlist.Duration, result.Duration);
                Assert.AreEqual(playlist.Picture, result.Picture);
            }
        }
    }
}
