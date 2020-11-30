using Microsoft.VisualStudio.TestTools.UnitTesting;
using PG.Data.Context;
using PG.Models;
using PG.Services;
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
            //TODO: seed data manualy :(
            //var options = Utils.GetOptions(nameof(GeneratePlaylistCorrectly));

            //int timeForTrip = 7200;
            //string playlistTitle = "Sofia - Sandanski";
            //int metalPercentagee = 25;
            //int rockPercentagee = 25;
            //int popPercentagee = 50;
            //int chalgaPercentage = 0;
            //bool topTracks = true;
            //bool sameArtist = true;
            //User user = new User();

            //var arrangeContext = new PGDbContext(options);
            //var _genreService = new GenreService(
            //    arrangeContext,
            //    new ArtistService(arrangeContext),
            //    new SongService(arrangeContext),
            //    new HttpDeezerClientService(new HttpClient())
            //    );

            //var deezerService = new DeezerAPIService(
            //    arrangeContext,
            //    _genreService,
            //    new ArtistService(arrangeContext),
            //    new SongService(arrangeContext),
            //    new HttpDeezerClientService(new HttpClient())
            //    );

            ////TODO: Не взима главния url от Startup.
            //await deezerService.ExtractSongsFromGenre("pop");
            //await deezerService.ExtractSongsFromGenre("rock");
            //await deezerService.ExtractSongsFromGenre("metal");

            //var sut = new PlaylistService( //Mock
            //    arrangeContext,
            //    new PixabayService(
            //        new HttpPixabayClientService(
            //            new HttpClient())
            //        )
            //    );

            //await sut.GeneratePlaylist(timeForTrip, playlistTitle, metalPercentagee, rockPercentagee, popPercentagee, chalgaPercentage, topTracks, sameArtist, user);
            //await arrangeContext.SaveChangesAsync();

            //var actual = arrangeContext.Playlists.FirstOrDefault(x => x.Id == 1);

            //Assert.AreEqual(1, actual.Id);
            //Assert.AreEqual(playlistTitle, actual.Title);
            //Assert.AreEqual(user.Id, actual.UserId);
            //Assert.IsTrue(actual.Duration < timeForTrip + 300);
            //Assert.IsTrue(actual.Duration > timeForTrip - 300);
            //Assert.IsTrue(actual.PlaylistsGenres.Count() == 3);
            //Assert.IsTrue(actual.PlaylistsSongs.Count() != 0);
        }
    }
}
