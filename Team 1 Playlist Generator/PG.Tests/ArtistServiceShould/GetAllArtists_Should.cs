using Microsoft.VisualStudio.TestTools.UnitTesting;
using PG.Data.Context;
using PG.Services;
using PG.Services.DTOs;
using System.Linq;
using System.Threading.Tasks;

namespace PG.Tests.ArtistServiceShould
{
    [TestClass]
    public class GetAllArtists_Should
    {
        [TestMethod]
        public async Task CorrectlyReturnsAllArtists()
        {
            //Arrange
            var options = Utils.GetOptions(nameof(CorrectlyReturnsAllArtists));
            var artistDTO = new ArtistDTO
            {
                Id = 1,
                Name = "Demi Lovato"
            };
            var artistDTO2 = new ArtistDTO
            {
                Id = 2,
                Name = "Demis Roussos"
            };

            using (var arrangeContext = new PGDbContext(options))
            {
                var sut = new ArtistService(arrangeContext);
                await sut.Create(artistDTO);
                await sut.Create(artistDTO2);
            }

            //Act
            using (var actContext = new PGDbContext(options))
            {
                var sut = new ArtistService(actContext);
                var result = await sut.GetAllArtists();


                //Assert
                Assert.AreEqual(actContext.Artists.Count(), result.Count());
                Assert.IsTrue(result.ElementAt(0).Id == 1);
                Assert.IsTrue(result.ElementAt(1).Id == 2);
            }
        }
    }
}
