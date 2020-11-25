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
    public class GetAllSongs_Should
    {
        [TestMethod]
        public async Task CorrectlyReturnsAllSongs()
        {
            //Arrange
            var options = Utils.GetOptions(nameof(CorrectlyReturnsAllSongs));
            var songDTO = new SongDTO
            {
                Id = 1,
                Title = "Tell me the truth",
                Duration = 130,
                Rank = 9000
            };
            var songDTO2 = new SongDTO
            {
                Id = 2,
                Title = "Let's have a party",
                Duration = 144,
                Rank = 9900
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
                var result = await sut.GetAllSongs();


                //Assert
                Assert.AreEqual(actContext.Songs.Count(), result.Count());
                Assert.IsTrue(result.ElementAt(0).Id == 1);
                Assert.IsTrue(result.ElementAt(1).Id == 2);
            }
        }
    }
}
