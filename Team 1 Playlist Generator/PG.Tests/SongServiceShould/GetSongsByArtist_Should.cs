using Microsoft.VisualStudio.TestTools.UnitTesting;
using PG.Data.Context;
using PG.Services;
using PG.Services.DTOs;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PG.Tests.SongServiceShould
{
    [TestClass]
    public class GetSongsByArtist_Should
    {
        [TestMethod]
        public async Task CorrectlyReturnsAllSongsByArtist()
        {
            //Arrange
            var options = Utils.GetOptions(nameof(CorrectlyReturnsAllSongsByArtist));
            var songDTO = new SongDTO
            {
                Id = 1,
                Title = "Tell me the truth",
                Duration = 130,
                Rank = 9000,
                ArtistId = 1
            };
            var songDTO2 = new SongDTO
            {
                Id = 2,
                Title = "Let's have a party",
                Duration = 144,
                Rank = 9900,
                ArtistId = 2
            };

            using (var arrangeContext = new PGDbContext(options))
            {
                var sut = new SongService(arrangeContext);
                await sut.Create(songDTO);
                await sut.Create(songDTO2);
            }

            //Act
            using (var actContext = new PGDbContext(options))
            {
                var sut = new SongService(actContext);
                var result = await sut.GetSongsByArtist(1);


                //Assert
                Assert.AreEqual(actContext.Songs.Where(x => x.ArtistId == 1).Count(), result.Count());
            }
        }
    }
}
