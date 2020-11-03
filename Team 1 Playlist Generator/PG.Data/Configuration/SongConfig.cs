using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PG.Models;
using System.Security.Cryptography.X509Certificates;

namespace PG.Data.Configuration
{
    public class SongConfig : IEntityTypeConfiguration<Song>
    {
        public void Configure(EntityTypeBuilder<Song> builder)
        {
            builder.HasKey(x => x.Id);


            //One to many   Playlist - Songs
            builder.HasOne(x => x.Playlist)
                .WithMany(x => x.Songs)
                .HasForeignKey(x => x.PlaylistId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
