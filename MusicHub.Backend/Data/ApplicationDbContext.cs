using System;
using Microsoft.EntityFrameworkCore;
using MusicHub.Backend.Models;

namespace MusicHub.Backend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }
    

        public DbSet<User> Users { get; set; }
        public DbSet<Songs> Songs { get; set; }
        public DbSet<SpotifySettings> SpotifySettings { get; set; }
        public DbSet<Playlist> Playlists { get; set; }

        // Configuring relationships and other settings.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure a one-to-many relationship between User and Song
            modelBuilder.Entity<Songs>()
                .HasOne(s => s.User)
                .WithMany(u => u.Songs)
                .HasForeignKey(s => s.UserId);

            // Configure a one-to-one relationship between User and SpotifySettings
            modelBuilder.Entity<SpotifySettings>()
                .HasOne(ss => ss.User)
                .WithOne(u => u.SpotifySettings)
                .HasForeignKey<SpotifySettings>(ss => ss.UserId);

            // Configure a one-to-many relationship between User and Playlist
            modelBuilder.Entity<Playlist>()
                .HasOne(p => p.User)
                .WithMany(u => u.Playlists)
                .HasForeignKey(p => p.UserId);

            // Configure a many-to-many relationship between Playlist and Song
            modelBuilder.Entity<Playlist>()
                .HasMany(p => p.Songs)
                .WithMany(s => s.Playlists);
        }
    }
}