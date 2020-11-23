using Microsoft.VisualStudio.TestTools.UnitTesting;
using PG.Data.Context;
using PG.Services;
using PG.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PG.Tests.GenreServiceShould
{
    [TestClass]
    public class UpdateShould
    {
        [TestMethod]
        public async Task UpdateCorrectly()
        {
            var options = Utils.GetOptions(nameof(UpdateCorrectly));

            var popGenre = new GenreDTO
            {
                Name = "Pop"
            };

            using (var arrangeContext = new PGDbContext(options))
            {
                var sut = new GenreService(arrangeContext);

                await sut.Create(popGenre);

                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new PGDbContext(options))
            {
                var sut = new GenreService(assertContext);

                var userPalylists = await sut.Update(1, popGenre);

                Assert.AreEqual(popGenre.Name, userPalylists.Name);

            }
        }

        [TestMethod]
        public async Task UpdateThrowsWhenNotFound()
        {
            var options = Utils.GetOptions(nameof(UpdateThrowsWhenNotFound));

            var popGenre = new GenreDTO
            {
                Name = "Pop"
            };
            var assertContext = new PGDbContext(options);

            var sut = new GenreService(assertContext);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.Update(1, popGenre));
        }
    }
}

