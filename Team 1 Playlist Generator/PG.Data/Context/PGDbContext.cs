using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PG.Models;
using System;

namespace PG.Data.Context
{
    public class PGDbContext : DbContext//Add Identity
    {
        public DbSet<Artist> Artist { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<PlaylistAndSongRelation> PlaylistAndSongRelations { get; set; }
        
        public PGDbContext(DbContextOptions<PGDbContext> options) : base(options)
        {

        }
        public PGDbContext()
        {

        }
    }
}
