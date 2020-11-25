using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PG.Data.Configurations;
using PG.Models;
using System;

namespace PG.Data.Context
{
    public class PGDbContext : IdentityDbContext<User>
    {
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<PlaylistsSongs> PlaylistSongs { get; set; }
        public DbSet<PlaylistsGenres> PlaylistsGenres { get; set; }
        public DbSet<Album> Albums { get; set; }

        public PGDbContext(DbContextOptions<PGDbContext> options) : base(options)
        {

        }
        public PGDbContext()
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new PlaylistsSongsConfig());

            builder.ApplyConfiguration(new PlaylistsGenresConfig());





            string id = new Guid().ToString();
            var hasher = new PasswordHasher<User>();
            var adminUser = new User();
            adminUser.Id = id;
            adminUser.UserName = "admin@admin.com";
            adminUser.NormalizedUserName = "ADMIN@ADMIN.COM";
            adminUser.Email = "admin@admin.com";
            adminUser.NormalizedEmail = "ADMIN@ADMIN.COM";
            adminUser.PasswordHash = hasher.HashPassword(adminUser, "admin123");
            adminUser.SecurityStamp = Guid.NewGuid().ToString();


            builder.Entity<User>().HasData(adminUser);

            builder.Entity<IdentityRole>().HasData(
           new IdentityRole { Id = "93ad4deb-b9f7-4a98-9585-8b79963aee55", Name = "User", NormalizedName = "USER", },
           new IdentityRole { Id = "6b32cc6d-2fc9-4808-a0a6-b3877bf9a381", Name = "Admin", NormalizedName = "ADMIN" });


            builder.Entity<IdentityUserRole<string>>().HasData(
                            new IdentityUserRole<string> { RoleId = "6b32cc6d-2fc9-4808-a0a6-b3877bf9a381", UserId = id }
                        );


            base.OnModelCreating(builder);
        }
    }
}
