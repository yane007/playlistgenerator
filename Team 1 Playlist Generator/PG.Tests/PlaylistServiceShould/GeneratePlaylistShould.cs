using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PG.Data.Context;
using PG.Models;
using PG.Services;
using PG.Services.Contract;
using PG.Services.Contracts.Helpers;
using PG.Services.Helpers;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PG.Tests.PlaylistServiceShould
{
    [TestClass]
    public class GeneratePlaylistShould
    {
        [TestMethod]
        public async Task GeneratePlaylistCorrectly()
        {
            var options = Utils.GetOptions(nameof(GeneratePlaylistCorrectly));

            var httpClientServiceMock = new Mock<IHttpDeezerClientService>();
            var songServiceMock = new Mock<ISongService>();
            var artistServiceMock = new Mock<IArtistService>();
            var pixabayServiceMock = new Mock<IPixabayService>();

            int timeForTrip = 2120;
            string playlistTitle = "Sofia - Sandanski";
            int metalPercentagee = 0;
            int rockPercentagee = 0;
            int popPercentagee = 0;
            int chalgaPercentage = 100;
            bool topTracks = true;
            bool sameArtist = true;
            User user = new User();

            var arrangeContext = new PGDbContext(options);


            var chalgaDb = await arrangeContext.Genres.AddAsync(new Genre { Name = "chalga" });

            for (int i = 0; i < 10; i++)
            {
                arrangeContext.Songs.Add(new Song
                {
                    Title = $"Ne me ostavyay{i}",
                    Duration = 212,
                    Rank = 100000,
                    GenreId = chalgaDb.Entity.Id,
                    Preview = "https://cdns-preview-2.dzcdn.net/stream/c-2d818454952b3d382ffd9467a61360ff-2.mp3"
                });
            }

            arrangeContext.SaveChanges();
            var sut = new PlaylistService(
                arrangeContext,
                pixabayServiceMock.Object);

            var image = "image";
            var id = 1;
            pixabayServiceMock.Setup(p => p.GetPixabayImage(id)).ReturnsAsync(image);

            await sut.GeneratePlaylist(timeForTrip, playlistTitle, metalPercentagee, rockPercentagee, popPercentagee, chalgaPercentage, topTracks, sameArtist, user);
            await arrangeContext.SaveChangesAsync();

            var actual = arrangeContext.Playlists.FirstOrDefault(x => x.Id == 1);

            Assert.AreEqual(1, actual.Id);
            Assert.AreEqual(playlistTitle, actual.Title);
            Assert.AreEqual(user.Id, actual.UserId);
            Assert.IsTrue(actual.Duration < timeForTrip + 300);
            Assert.IsTrue(actual.Duration > timeForTrip - 300);
            Assert.IsTrue(actual.PlaylistsGenres.Count() == 1);
            Assert.IsTrue(actual.PlaylistsSongs.Count() != 0);
        }
    }
}
