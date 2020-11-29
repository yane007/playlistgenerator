using Microsoft.VisualStudio.TestTools.UnitTesting;
using PG.Data.Context;
using PG.Services;
using PG.Services.DTOs;
using PG.Services.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PG.Tests.ArtistServiceShould
{
    [TestClass]
    public class UpdateArtist_Should
    {
        [TestMethod]
        public async Task ThrowIfArtistIdDoesNotExists()
        {
            //Arrange
            var options = Utils.GetOptions(nameof(ThrowIfArtistIdDoesNotExists));

            var artistDTO = new ArtistDTO
            {
                Id = 1,
                Name = "Janet Jackson"
            };

            //Act & Assert
            using (var actContext = new PGDbContext(options))
            {
                var sut = new ArtistService(actContext);

                await Assert.ThrowsExceptionAsync<NotFoundException>(() => sut.Update(2, artistDTO));
            }
        }

        [TestMethod]
        public async Task CorrectlyUpdatesArtist()
        {
            //Arrange
            var options = Utils.GetOptions(nameof(CorrectlyUpdatesArtist));
            var artistDTO = new ArtistDTO
            {
                Id = 1,
                Name = "Demi Lovato"
            };
            var modelToUpdateWith = new ArtistDTO
            {
                Name = "Demis Roussos"
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
                var result = await sut.Update(1, modelToUpdateWith);

                Assert.AreEqual(actContext.Artists.First().Name, result.Name);
            }
        }
    }
}
