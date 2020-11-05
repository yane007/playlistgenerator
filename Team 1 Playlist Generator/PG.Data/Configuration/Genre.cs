using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PG.Models;

namespace PG.Data.Configuration
{
    public class Genre : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            //builder.HasKey(x => x.Id);
        }
    }
}
