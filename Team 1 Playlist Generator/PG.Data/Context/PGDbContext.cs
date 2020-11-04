using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PG.Models;
using System;

namespace PG.Data.Context
{
    public class PGDbContext : IdentityDbContext//Add Identity
    {
        public DbSet<Album> Albums { get; set; }
        public DbSet<Creator> Creators { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public PGDbContext(DbContextOptions<PGDbContext> options) : base(options)
        {

        }
        public PGDbContext()
        {

        }
    }
}
