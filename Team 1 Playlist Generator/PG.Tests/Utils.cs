using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PG.Data.Context;

namespace PG.Tests
{
    [TestClass]
    public class Utils
    {
        public static DbContextOptions<PGDbContext> GetOptions(string databaseName)
        {
            return new DbContextOptionsBuilder<PGDbContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;
        }
    }
}
