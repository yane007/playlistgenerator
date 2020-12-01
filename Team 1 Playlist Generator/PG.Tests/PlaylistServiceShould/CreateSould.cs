using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PG.Data.Context;
using PG.Services;
using PG.Services.DTOs;
using PG.Services.Exceptions;
using PG.Services.Helpers;
using System;
using System.Net.Http;
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
            };

            using (var arrangeContext = new PGDbContext(options))
            {

                var pixabayServiceMock = new Mock<IPixabayService>();


                var sut = new PlaylistService(arrangeContext,  pixabayServiceMock.Object);

                await sut.Create(playlist);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new PGDbContext(options))
            {
                var result = await assertContext.Playlists.FirstOrDefaultAsync(x => x.Title == playlist.Title);

                Assert.AreEqual(playlist.Title, result.Title);
                Assert.AreEqual(playlist.Duration, result.Duration);
            }
        }

        [TestMethod]
        public async Task CreateThrowsWhenNull()
        {
            var options = Utils.GetOptions(nameof(CreateThrowsWhenNull));

            var context = new PGDbContext(options);
            var pixabayServiceMock = new Mock<IPixabayService>();

            var sut = new PlaylistService(context, pixabayServiceMock.Object);

            await Assert.ThrowsExceptionAsync<NotFoundException>(() => sut.Create(null));
        }

        [TestMethod]
        public async Task CreateThrowsWhenNameAbove50Chars()
        {
            var options = Utils.GetOptions(nameof(CreateThrowsWhenNameAbove50Chars));

            var context = new PGDbContext(options);
            var pixabayServiceMock = new Mock<IPixabayService>();

            var sut = new PlaylistService(context, pixabayServiceMock.Object);

            var playlist = new PlaylistDTO
            {
                Title = new String('T',52)
            };

            await Assert.ThrowsExceptionAsync<OutOfRangeException>(() => sut.Create(playlist));
        }
    }
}
