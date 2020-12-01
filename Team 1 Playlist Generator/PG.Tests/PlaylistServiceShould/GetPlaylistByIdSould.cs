using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PG.Data.Context;
using PG.Models;
using PG.Services;
using PG.Services.DTOs;
using PG.Services.Exceptions;
using PG.Services.Helpers;
using System;
using System.Linq;
using System.Net.Http;
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
            var pixabayServiceMock = new Mock<IPixabayService>();

            var firstUser = new User
            {
                UserName = "FirstUser",
            };

            string firstUserId = string.Empty;
 
            using (var arrangeContext = new PGDbContext(options))
            {
                var firstUserBd = await arrangeContext.Users.AddAsync(firstUser);
 
                firstUserId = firstUserBd.Entity.Id;
 
                var nirvanaPlaylist = new PlaylistDTO
                {
                    Title = "In Utero",
                    Duration = 1600,
                    UserId = firstUserId,
                };

                var acdcPlaylist = new PlaylistDTO
                {
                    Title = "Back in Black",
                    Duration = 2531,
                    UserId = firstUserId,
                };

                var sut = new PlaylistService(arrangeContext, pixabayServiceMock.Object);

                await sut.Create(nirvanaPlaylist);
                await sut.Create(acdcPlaylist);

                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new PGDbContext(options))
            {
                var sut = new PlaylistService(assertContext, pixabayServiceMock.Object);

                var userPalylists = await sut.GetPlaylistById(1);

                Assert.AreEqual("In Utero", userPalylists.Title);
                Assert.AreEqual(1600, userPalylists.Duration);
            }
        }

        [TestMethod]
        public async Task GetPlaylistByIdThrowsWhenNotFound()
        {
            var options = Utils.GetOptions(nameof(GetPlaylistByIdThrowsWhenNotFound));
            var pixabayServiceMock = new Mock<IPixabayService>();
            var assertContext = new PGDbContext(options);

            var sut = new PlaylistService(assertContext, pixabayServiceMock.Object);

            await Assert.ThrowsExceptionAsync<NotFoundException>(() => sut.GetPlaylistById(1));
        }
    }
}
