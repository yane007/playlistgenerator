using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PG.Data.Context;
using PG.Models;
using PG.Services;
using PG.Services.DTOs;
using PG.Services.Exceptions;
using System.Threading.Tasks;

namespace PG.Tests.PlaylistServiceShould
{
    [TestClass]
    public class UpdatePublicAccessShould
    {
        [TestMethod]
        public async Task UpdatePublicAccessCorrectlyToPrivate()
        {
            var options = Utils.GetOptions(nameof(UpdatePublicAccessCorrectlyToPrivate));
            var pixabayServiceMock = new Mock<IPixabayService>();

            var firstUser = new User
            {
                UserName = "FirstUser",
            };

            using (var arrangeContext = new PGDbContext(options))
            {
                var firstUserBd = await arrangeContext.Users.AddAsync(firstUser);

                var nirvanaPlaylist = new PlaylistDTO
                {
                    Title = "In Utero",
                    Duration = 1600,
                    UserId = firstUserBd.Entity.Id,
                };

                var sut = new PlaylistService(arrangeContext, pixabayServiceMock.Object);

                await sut.Create(nirvanaPlaylist);

                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new PGDbContext(options))
            {
                var sut = new PlaylistService(assertContext, pixabayServiceMock.Object);

                await sut.UpdatePublicAccess(1);

                var updatedPlaylist = await sut.GetPlaylistById(1);

                Assert.AreEqual(false, updatedPlaylist.IsPublic);
            }
        }

        [TestMethod]
        public async Task UpdatePublicAccessCorrectlyToPublic()
        {
            var options = Utils.GetOptions(nameof(UpdatePublicAccessCorrectlyToPublic));
            var pixabayServiceMock = new Mock<IPixabayService>();

            var firstUser = new User
            {
                UserName = "FirstUser",
            };

            using (var arrangeContext = new PGDbContext(options))
            {
                var firstUserBd = await arrangeContext.Users.AddAsync(firstUser);

                var nirvanaPlaylist = new PlaylistDTO
                {
                    Title = "In Utero",
                    Duration = 1600,
                    UserId = firstUserBd.Entity.Id,
                };

                var sut = new PlaylistService(arrangeContext, pixabayServiceMock.Object);

                await sut.Create(nirvanaPlaylist);

                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new PGDbContext(options))
            {
                var sut = new PlaylistService(assertContext, pixabayServiceMock.Object);

                await sut.UpdatePublicAccess(1);
                await sut.UpdatePublicAccess(1);

                var updatedPlaylist = await sut.GetPlaylistById(1);

                Assert.AreEqual(true, updatedPlaylist.IsPublic);
            }
        }

        [TestMethod]
        public async Task UpdatePublicAccessCorrectlyThrowsWhenNoPlaylistFound()
        {
            var options = Utils.GetOptions(nameof(UpdatePublicAccessCorrectlyThrowsWhenNoPlaylistFound));
            var pixabayServiceMock = new Mock<IPixabayService>();

            var assertContext = new PGDbContext(options);

            var sut = new PlaylistService(assertContext, pixabayServiceMock.Object);

            await Assert.ThrowsExceptionAsync<NotFoundException>(() => sut.UpdatePublicAccess(1));

        }
    }
}
