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
    public class CreateArtist_Should
    {
        [TestMethod]
        public async Task ReturnCorrectlyWhenParamsAreValid()
        {
            //Arrange
            var options = Utils.GetOptions(nameof(ReturnCorrectlyWhenParamsAreValid));
            var artistDTO = new ArtistDTO
            {
                Id = 1,
                Name = "David Bowie"
            };

            //Act
            using (var actContext = new PGDbContext(options))
            {
                var sut = new ArtistService(actContext);
                var result = await sut.Create(artistDTO);

                //Assert
                Assert.IsTrue(actContext.Artists.Count() == 1);
                Assert.AreEqual(actContext.Artists.First().Id, result.Id);
                Assert.AreEqual(actContext.Artists.First().Name, result.Name);
            }
        }

        [TestMethod]
        public async Task ThrowWhenArtistExists()
        {
            //Arrange
            var options = Utils.GetOptions(nameof(ThrowWhenArtistExists));
            var artistDTO = new ArtistDTO
            {
                Id = 1,
                Name = "David Bowie"
            };

            //Act & Assert
            using (var actContext = new PGDbContext(options))
            {
                var sut = new ArtistService(actContext);
                var temp = await sut.Create(artistDTO);

                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.Create(artistDTO));
            }
        }

        [TestMethod]
        public async Task ThrowWhenArtistNameIsMoreThan100Chars()
        {
            //Arrange
            var options = Utils.GetOptions(nameof(ThrowWhenArtistNameIsMoreThan100Chars));
            var artistDTO = new ArtistDTO
            {
                Id = 1,
                Name = "David Bowie - Test example of an artist name with more than hundred characters!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!"
            };

            //Act & Assert
            using (var actContext = new PGDbContext(options))
            {
                var sut = new ArtistService(actContext);

                await Assert.ThrowsExceptionAsync<OutOfRangeException>(() => sut.Create(artistDTO));
            }
        }

        [TestMethod]
        public async Task ThrowWhenArtistIsNull()
        {
            var options = Utils.GetOptions(nameof(ThrowWhenArtistIsNull));

            var context = new PGDbContext(options);
            var sut = new ArtistService(context);

            await Assert.ThrowsExceptionAsync<NotFoundException>(() => sut.Create(null));
        }
    }
}
