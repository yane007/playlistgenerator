using Microsoft.VisualStudio.TestTools.UnitTesting;
using PG.Data.Context;
using PG.Services;
using PG.Services.DTOs;
using System;
using System.Threading.Tasks;

namespace PG.Tests.ArtistServiceShould
{
    [TestClass]
    public class GetArtistById_Should
    {
        [TestMethod]
        public async Task ThrowIfArtistIdDoesNotExists()
        {
            //Arrange
            var options = Utils.GetOptions(nameof(ThrowIfArtistIdDoesNotExists));

            //Act & Assert
            using (var actContext = new PGDbContext(options))
            {
                var sut = new ArtistService(actContext);

                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.GetArtistById(2));
            }
        }

        [TestMethod]
        public async Task CorrectlyReturnsAnArtist()
        {
            //Arrange
            var options = Utils.GetOptions(nameof(CorrectlyReturnsAnArtist));
            var artistDTO = new ArtistDTO
            {
                Id = 1,
                Name = "Demi Lovato"
            };

            using (var arrangeContext = new PGDbContext(options))
            {
                var sut = new ArtistService(arrangeContext);
                await sut.Create(artistDTO);
            }

            //Act & Assert
            using (var actContext = new PGDbContext(options))
            {
                var sut = new ArtistService(actContext);
                var result = await sut.GetArtistById(1);

                Assert.AreEqual(artistDTO.Id, result.Id);
                Assert.AreEqual(artistDTO.Name, result.Name);
            }
        }
    }
}
