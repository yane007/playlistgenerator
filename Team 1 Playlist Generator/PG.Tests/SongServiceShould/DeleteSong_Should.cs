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
    public class DeleteSong_Should
    {
        [TestMethod]
        public async Task CorrectlySetIsDeletedStatus()
        {
            //Arrange
            var options = Utils.GetOptions(nameof(CorrectlySetIsDeletedStatus));

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

            using (var actContext = new PGDbContext(options))
            {
                var sut = new SongService(actContext);
                var song = actContext.Songs.First(x => x.Id == 1);
                Assert.IsFalse(song.IsDeleted);
                await sut.Delete(1);
                Assert.IsTrue(song.IsDeleted);
            }
        }

        [TestMethod]
        public async Task ThrowIfSongIsAlreadyDeleted()
        {
            //Arrange
            var options = Utils.GetOptions(nameof(ThrowIfSongIsAlreadyDeleted));

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
                await sut.Delete(1);
            }

            using (var actContext = new PGDbContext(options))
            {
                var sut = new SongService(actContext);
                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.Delete(1));
            }
        }

        [TestMethod]
        public async Task ThrowIfSongDoesNotExists()
        {
            //Arrange
            var options = Utils.GetOptions(nameof(ThrowIfSongDoesNotExists));

            //Act & Assert
            using (var actContext = new PGDbContext(options))
            {
                var sut = new SongService(actContext);

                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.Delete(2));
            }
        }
    }
}
