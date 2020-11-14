using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PG.Models;

namespace PG.Data.Configurations
{
    public class PlaylistsSongsConfig : IEntityTypeConfiguration<PlaylistsSongs>
    {
        public void Configure(EntityTypeBuilder<PlaylistsSongs> builder)
        {
            builder.HasKey(z => new { z.PlaylistId, z.SongId });

        }
    }

}
