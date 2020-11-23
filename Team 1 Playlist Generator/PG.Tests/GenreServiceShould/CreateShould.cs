using Microsoft.EntityFrameworkCore;
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
    public class CreateShould
    {
        [TestMethod]
        public async Task CreateCorrectly()
        {
            var options = Utils.GetOptions(nameof(CreateCorrectly));

            var genre = new GenreDTO
            {
                Name = "Jazz"
            };

            using (var arrangeContext = new PGDbContext(options))
            {
                var sut = new GenreService(arrangeContext);
                await sut.Create(genre);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new PGDbContext(options))
            {
                var result = await assertContext.Genres.FirstOrDefaultAsync(x => x.Name == genre.Name);

                Assert.AreEqual(genre.Name, result.Name);

            }
        }

        [TestMethod]
        public async Task CreateThrowsWhenNull()
        {
            var options = Utils.GetOptions(nameof(CreateThrowsWhenNull));

            var context = new PGDbContext(options);
            var sut = new GenreService(context);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.Create(null));

        }

        [TestMethod]
        public async Task CreateThrowsWhenGenreWithSameNameExists()
        {
            var options = Utils.GetOptions(nameof(CreateThrowsWhenGenreWithSameNameExists));

            var genre = new GenreDTO
            {
                Name = "Jazz"
            };

            using (var arrangeContext = new PGDbContext(options))
            {
                var sut = new GenreService(arrangeContext);
                await sut.Create(genre);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new PGDbContext(options))
            {
                var sut = new GenreService(assertContext);

                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.Create(genre));
            }
        }
    }
}

