using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PG.Models;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace PG.Data.Configuration
{
    public class PlaylistAndSongRelationConfig : IEntityTypeConfiguration<PlaylistsSongs>
    {
        public void Configure(EntityTypeBuilder<PlaylistsSongs> builder)
        {
            builder.HasKey(z => new { z.PlaylistId, z.SongId});

            //One to many PlaylistAndSongRelations - Song
            builder.HasOne(z => z.Song)
                .WithMany(z => z.PlaylistsSongs)
                .HasForeignKey(x => x.SongId)
                .OnDelete(DeleteBehavior.Restrict);

            //One to many PlaylistAndSongRelation - Playlist
            builder.HasOne(x => x.Playlist)
                .WithMany(x => x.PlaylistsSongs)
                .HasForeignKey(x => x.PlaylistId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
