using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PG.Data.Context;
using PG.Models;
using PG.Services;
using PG.Services.DTOs;
using System.Linq;
using System.Threading.Tasks;

namespace PG.Tests.PlaylistServiceShould
{
    [TestClass]
    public class GetAllPlaylistsWithSettingsShould
    {
        [TestMethod]
        public async Task GetAllPlaylistsWithSettingsCorrectlyByTitle()
        {
            var options = Utils.GetOptions(nameof(GetAllPlaylistsWithSettingsCorrectlyByTitle));
            var pixabayServiceMock = new Mock<IPixabayService>();

            var firstUser = new User
            {
                UserName = "FirstUser",
            };

            using (var arrangeContext = new PGDbContext(options))
            {
                var firstUserBd = await arrangeContext.Users.AddAsync(firstUser);

                var firstUserId = firstUserBd.Entity.Id;

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

                var scorpionsPLaylist = new PlaylistDTO
                {
                    Title = "Lovedrive",
                    Duration = 2190,
                    UserId = firstUserId,
                };


                var sut = new PlaylistService(arrangeContext, pixabayServiceMock.Object);

                await sut.Create(nirvanaPlaylist);
                await sut.Create(acdcPlaylist);
                await sut.Create(scorpionsPLaylist);

                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new PGDbContext(options))
            {
                var sut = new PlaylistService(assertContext, pixabayServiceMock.Object);

                string nameLike = "In";
                string genre  = "";
                string duration  = "";

                var userPalylists = await sut.GetAllPlaylistsWithSettings(nameLike, genre, duration);
                int userPalylistsCount = userPalylists.Count();

                Assert.AreEqual(1, userPalylistsCount);
                Assert.AreEqual("In Utero", userPalylists.FirstOrDefault().Title);
            }
        }

        [TestMethod]
        public async Task GetAllPlaylistsWithSettingsCorrectlyDuration()
        {
            var options = Utils.GetOptions(nameof(GetAllPlaylistsWithSettingsCorrectlyDuration));
            var pixabayServiceMock = new Mock<IPixabayService>();

            var firstUser = new User
            {
                UserName = "FirstUser",
            };

            using (var arrangeContext = new PGDbContext(options))
            {
                var firstUserBd = await arrangeContext.Users.AddAsync(firstUser);

                var firstUserId = firstUserBd.Entity.Id;

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

                var scorpionsPLaylist = new PlaylistDTO
                {
                    Title = "Lovedrive",
                    Duration = 2190,
                    UserId = firstUserId,
                };


                var sut = new PlaylistService(arrangeContext, pixabayServiceMock.Object);

                await sut.Create(nirvanaPlaylist);
                await sut.Create(acdcPlaylist);
                await sut.Create(scorpionsPLaylist);

                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new PGDbContext(options))
            {
                var sut = new PlaylistService(assertContext, pixabayServiceMock.Object);

                string nameLike = "";
                string genre = "";
                string duration = "2190";

                var userPalylists = await sut.GetAllPlaylistsWithSettings(nameLike, genre, duration);
                int userPalylistsCount = userPalylists.Count();

                Assert.AreEqual(2, userPalylistsCount);
            }
        }

        [TestMethod]
        public async Task GetAllPlaylistsWithSettingsReturnsEmptyStringNotFound()
        {
            var options = Utils.GetOptions(nameof(GetAllPlaylistsWithSettingsReturnsEmptyStringNotFound));
            var pixabayServiceMock = new Mock<IPixabayService>();

            var firstUser = new User
            {
                UserName = "FirstUser",
            };

            using (var arrangeContext = new PGDbContext(options))
            {
                var firstUserBd = await arrangeContext.Users.AddAsync(firstUser);

                var firstUserId = firstUserBd.Entity.Id;

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

                var scorpionsPLaylist = new PlaylistDTO
                {
                    Title = "Lovedrive",
                    Duration = 2190,
                    UserId = firstUserId,
                };


                var sut = new PlaylistService(arrangeContext, pixabayServiceMock.Object);

                await sut.Create(nirvanaPlaylist);
                await sut.Create(acdcPlaylist);
                await sut.Create(scorpionsPLaylist);

                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new PGDbContext(options))
            {
                var sut = new PlaylistService(assertContext, pixabayServiceMock.Object);

                string nameLike = "Cant't find this";
                string genre = "";
                string duration = "";

                var userPalylists = await sut.GetAllPlaylistsWithSettings(nameLike, genre, duration);
                int userPalylistsCount = userPalylists.Count();

                Assert.AreEqual(0, userPalylistsCount);
            }
        }

        [TestMethod]
        public async Task GetAllPlaylistsWithSettingsReturnsEmptyNoDurationMatched()
        {
            var options = Utils.GetOptions(nameof(GetAllPlaylistsWithSettingsReturnsEmptyNoDurationMatched));
            var pixabayServiceMock = new Mock<IPixabayService>();

            var firstUser = new User
            {
                UserName = "FirstUser",
            };

            using (var arrangeContext = new PGDbContext(options))
            {
                var firstUserBd = await arrangeContext.Users.AddAsync(firstUser);

                var firstUserId = firstUserBd.Entity.Id;

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

                var scorpionsPLaylist = new PlaylistDTO
                {
                    Title = "Lovedrive",
                    Duration = 2190,
                    UserId = firstUserId,
                };


                var sut = new PlaylistService(arrangeContext, pixabayServiceMock.Object);

                await sut.Create(nirvanaPlaylist);
                await sut.Create(acdcPlaylist);
                await sut.Create(scorpionsPLaylist);

                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new PGDbContext(options))
            {
                var sut = new PlaylistService(assertContext, pixabayServiceMock.Object);

                string nameLike = "";
                string genre = "";
                string duration = "1080";

                var userPalylists = await sut.GetAllPlaylistsWithSettings(nameLike, genre, duration);
                int userPalylistsCount = userPalylists.Count();

                Assert.AreEqual(0, userPalylistsCount);
            }
        }

        [TestMethod]
        public async Task GetAllPlaylistsWithSettingsReturnsEverythingWhenNoParameters()
        {
            var options = Utils.GetOptions(nameof(GetAllPlaylistsWithSettingsReturnsEverythingWhenNoParameters));
            var pixabayServiceMock = new Mock<IPixabayService>();

            var firstUser = new User
            {
                UserName = "FirstUser",
            };

            using (var arrangeContext = new PGDbContext(options))
            {
                var firstUserBd = await arrangeContext.Users.AddAsync(firstUser);

                var firstUserId = firstUserBd.Entity.Id;

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

                var scorpionsPLaylist = new PlaylistDTO
                {
                    Title = "Lovedrive",
                    Duration = 2190,
                    UserId = firstUserId,
                };


                var sut = new PlaylistService(arrangeContext, pixabayServiceMock.Object);

                await sut.Create(nirvanaPlaylist);
                await sut.Create(acdcPlaylist);
                await sut.Create(scorpionsPLaylist);

                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new PGDbContext(options))
            {
                var sut = new PlaylistService(assertContext, pixabayServiceMock.Object);

                string nameLike = "";
                string genre = "";
                string duration = "";

                var userPalylists = await sut.GetAllPlaylistsWithSettings(nameLike, genre, duration);
                int userPalylistsCount = userPalylists.Count();

                Assert.AreEqual(3, userPalylistsCount);
            }
        }
    }
}
