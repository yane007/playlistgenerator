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
    public class GetSongById_Should
    {
        [TestMethod]
        public async Task ThrowIfSongIdDoesNotExists()
        {
            //Arrange
            var options = Utils.GetOptions(nameof(ThrowIfSongIdDoesNotExists));

            //Act & Assert
            using (var actContext = new PGDbContext(options))
            {
                var sut = new SongService(actContext);

                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.GetSongById(2));
            }
        }

        [TestMethod]
        public async Task CorrectlyReturnsAnSong()
        {
            //Arrange
            var options = Utils.GetOptions(nameof(CorrectlyReturnsAnSong));
            var songDTO = new SongDTO
            {
                Id = 1,
                Title = "Tell me the truth",
                Duration = 130,
                Rank = 9000
            };

            using (var arrangeContext = new PGDbContext(options))
            {
                var sut = new SongService(arrangeContext);
                await sut.Create(songDTO);
            }

            //Act
            using (var actContext = new PGDbContext(options))
            {
                var sut = new SongService(actContext);
                var result = await sut.GetSongById(1);


                //Assert
                Assert.AreEqual(songDTO.Id, result.Id);
                Assert.AreEqual(songDTO.Title, result.Title);
                Assert.AreEqual(songDTO.Duration, result.Duration);
                Assert.AreEqual(songDTO.Rank, result.Rank);
            }
        }
    }
}
