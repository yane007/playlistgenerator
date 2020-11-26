using Microsoft.VisualStudio.TestTools.UnitTesting;
using PG.Data.Context;
using PG.Models;
using PG.Services;
using PG.Services.DTOs;
using System;
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

            int timeForTrip = 7200;
            string playlistTitle = "Sofia - Sandanski";
            int metalPercentagee = 25;
            int rockPercentagee = 25;
            int popPercentagee = 50;
            bool topTracks = true;
            bool sameArtist = true;
            User user = new User();

            var arrangeContext = new PGDbContext(options);
            var _genreService = new GenreService(arrangeContext, new ArtistService(arrangeContext), new SongService(arrangeContext));

            var deezerService = new DeezerAPIService(arrangeContext, _genreService,
                new ArtistService(arrangeContext), new SongService(arrangeContext));

            //TODO: 
            await deezerService.ExtractSongsFromGenre("pop");
            await deezerService.ExtractSongsFromGenre("rock");
            await deezerService.ExtractSongsFromGenre("metal");

            var sut = new PlaylistService(arrangeContext);

            await sut.GeneratePlaylist(timeForTrip, playlistTitle, metalPercentagee, rockPercentagee, popPercentagee, topTracks, sameArtist, user);
            await arrangeContext.SaveChangesAsync();

            var actual = arrangeContext.Playlists.FirstOrDefault(x => x.Id == 1);

            Assert.AreEqual(1, actual.Id);
            Assert.AreEqual(playlistTitle, actual.Title);
            Assert.AreEqual(user.Id, actual.UserId);
            Assert.IsTrue(actual.Duration < timeForTrip + 300);
            Assert.IsTrue(actual.Duration > timeForTrip - 300);
            Assert.IsTrue(actual.PlaylistsGenres.Count() == 3);
            Assert.IsTrue(actual.PlaylistsSongs.Count() != 0);

        }
    }
}
