using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PG.Models;

namespace PG.Data.Configurations
{
    public class PlaylistsGenresConfig : IEntityTypeConfiguration<PlaylistsGenres>
    {
        public void Configure(EntityTypeBuilder<PlaylistsGenres> builder)
        {
            builder.HasKey(x => new { x.PlaylistId, x.GenreId });
        } 
    }
}
