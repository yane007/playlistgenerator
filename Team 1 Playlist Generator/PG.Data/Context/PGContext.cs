using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PG.Models;
using System;

namespace PG.Data.Context
{
    public class PGContext : DbContext//Add Identity
    {
        public DbSet<Album> Albums { get; set; }
        public DbSet<Creator> Creators { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<Song> Songs { get; set; }
        public PGContext(DbContextOptions<PGContext> options) : base(options)
        {

        }
        public PGContext()
        {

        }
    }
}
