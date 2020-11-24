using Microsoft.VisualStudio.TestTools.UnitTesting;
using PG.Data.Context;
using PG.Services;
using PG.Services.DTOs;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PG.Tests.ArtistServiceShould
{
    [TestClass]
    public class DeleteArtist_Should
    {
        [TestMethod]
        public async Task CorrectlySetIsDeletedStatus()
        {
            //Arrange
            var options = Utils.GetOptions(nameof(CorrectlySetIsDeletedStatus));

            var artistDTO = new ArtistDTO
            {
                Id = 1,
                Name = "Dean Martin"
            };

            using (var arrangeContext = new PGDbContext(options))
            {
                var sut = new ArtistService(arrangeContext);
                await sut.Create(artistDTO);
            }

            using (var actContext = new PGDbContext(options))
            {
                var sut = new ArtistService(actContext);
                var artist = actContext.Artists.First(x => x.Id == 1);
                Assert.IsFalse(artist.IsDeleted);
                await sut.Delete(1);
                Assert.IsTrue(artist.IsDeleted);
            }
        }

        [TestMethod]
        public async Task ThrowIfArtistIsAlreadyDeleted()
        {
            //Arrange
            var options = Utils.GetOptions(nameof(ThrowIfArtistIsAlreadyDeleted));

            var artistDTO = new ArtistDTO
            {
                Id = 2,
                Name = "Dean Martin2"
            };

            using (var arrangeContext = new PGDbContext(options))
            {
                var sut = new ArtistService(arrangeContext);
                await sut.Create(artistDTO);
                await sut.Delete(2);
            }

            using (var actContext = new PGDbContext(options))
            {
                var sut = new ArtistService(actContext);
                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.Delete(2));
            }
        }

        [TestMethod]
        public async Task ThrowIfArtistDoesNotExists()
        {
            //Arrange
            var options = Utils.GetOptions(nameof(ThrowIfArtistDoesNotExists));

            //Act & Assert
            using (var actContext = new PGDbContext(options))
            {
                var sut = new ArtistService(actContext);

                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.Delete(2));
            }
        }
    }
}
