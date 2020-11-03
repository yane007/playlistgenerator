using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PG.Models;


namespace PG.Data.Configuration
{
    public class CreatorConfig : IEntityTypeConfiguration<Creator>
    {
        public void Configure(EntityTypeBuilder<Creator> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
