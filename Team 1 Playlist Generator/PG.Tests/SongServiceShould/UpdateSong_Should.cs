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
    public class UpdateSong_Should
    {
        [TestMethod]
        public async Task ThrowIfSongIdDoesNotExists()
        {
            //Arrange
            var options = Utils.GetOptions(nameof(ThrowIfSongIdDoesNotExists));

            var songDTO = new SongDTO
            {
                Id = 1,
                Title = "Tell me the truth",
                Duration = 130,
                Rank = 9000
            };

            //Act & Assert
            using (var actContext = new PGDbContext(options))
            {
                var sut = new SongService(actContext);

                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.Update(2, songDTO));
            }
        }

        [TestMethod]
        public async Task CorrectlyUpdatesSong()
        {
            //Arrange
            var options = Utils.GetOptions(nameof(CorrectlyUpdatesSong));
            var songDTO = new SongDTO
            {
                Id = 1,
                Title = "Tell me the truth",
                Duration = 130,
                Rank = 9000
            };
            var modelToUpdateWith = new SongDTO
            {
                Title = "Who is there",
                Duration = 170,
                Rank = 7000
            };

            using (var arrangeContext = new PGDbContext(options))
            {
                var sut = new SongService(arrangeContext);
                await sut.Create(songDTO);
            }

            //Act & Assert
            using (var actContext = new PGDbContext(options))
            {
                var sut = new SongService(actContext);
                var result = await sut.Update(1, modelToUpdateWith);

                Assert.AreEqual(actContext.Songs.First().Title, result.Title);
                Assert.AreEqual(actContext.Songs.First().Duration, result.Duration);
                Assert.AreEqual(actContext.Songs.First().Rank, result.Rank);
            }
        }
    }
}
