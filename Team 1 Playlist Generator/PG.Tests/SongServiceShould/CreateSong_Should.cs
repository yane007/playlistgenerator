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
    public class CreateSong_Should
    {
        [TestMethod]
        public async Task ReturnCorrectlyWhenParamsAreValid()
        {
            //Arrange
            var options = Utils.GetOptions(nameof(ReturnCorrectlyWhenParamsAreValid));
            var songDTO = new SongDTO
            {
                Id = 1,
                Title = "Can't Believe",
                Duration = 150,
                Rank = 10000
            };

            //Act
            using (var actContext = new PGDbContext(options))
            {
                var sut = new SongService(actContext);
                var result = await sut.Create(songDTO);

                //Assert
                Assert.IsTrue(actContext.Songs.Count() == 1);
                Assert.AreEqual(actContext.Songs.First().Id, result.Id);
                Assert.AreEqual(actContext.Songs.First().Title, result.Title);
                Assert.AreEqual(actContext.Songs.First().Duration, result.Duration);
                Assert.AreEqual(actContext.Songs.First().Rank, result.Rank);
            }
        }

        [TestMethod]
        public async Task ThrowWhenSongExists()
        {
            //Arrange
            var options = Utils.GetOptions(nameof(ThrowWhenSongExists));
            var songDTO = new SongDTO
            {
                Id = 1,
                Title = "Something Wrong",
                Duration = 150,
                Rank = 10000
            };

            //Act & Assert
            using (var actContext = new PGDbContext(options))
            {
                var sut = new SongService(actContext);
                var temp = await sut.Create(songDTO);

                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.Create(songDTO));
            }
        }

        [TestMethod]
        public async Task ThrowWhenSongTitleIsMoreThan200Chars()
        {
            //Arrange
            var options = Utils.GetOptions(nameof(ThrowWhenSongTitleIsMoreThan200Chars));
            var songDTO = new SongDTO
            {
                Id = 1,
                Title = new String('T', 205),
                Duration = 150,
                Rank = 10000
            };

            //Act & Assert
            using (var actContext = new PGDbContext(options))
            {
                var sut = new SongService(actContext);

                await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(() => sut.Create(songDTO));
            }
        }

        [TestMethod]
        public async Task ThrowWhenSongIsNull()
        {
            var options = Utils.GetOptions(nameof(ThrowWhenSongIsNull));

            var context = new PGDbContext(options);
            var sut = new SongService(context);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.Create(null));
        }
    }
}
