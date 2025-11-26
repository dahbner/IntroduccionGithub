using Microsoft.EntityFrameworkCore;
using MiniSpotify.Models;

namespace MiniSpotify.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Artist> Artists { get; set; } 
        public DbSet<ArtistDetail> ArtistDetails { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Playlist> Playlists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // 1. User ↔ Playlist (1:N)
            modelBuilder.Entity<User>()
                .HasMany(u => u.Playlists)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // 2. Artist ↔ ArtistDetail (1:1)
            modelBuilder.Entity<Artist>()
                .HasOne(a => a.ArtistDetail)
                .WithOne(ad => ad.Artist)
                .HasForeignKey<ArtistDetail>(ad => ad.ArtistId)
                .OnDelete(DeleteBehavior.Cascade);

            // 3. Artist ↔ Album (1:N)
            modelBuilder.Entity<Artist>()
                .HasMany(a => a.Albums)
                .WithOne(al => al.Artist)
                .HasForeignKey(al => al.ArtistId)
                .OnDelete(DeleteBehavior.Cascade);

            // 4. Album ↔ Song (1:N)
            modelBuilder.Entity<Album>()
                .HasMany(a => a.Songs)
                .WithOne(s => s.Album)
                .HasForeignKey(s => s.AlbumId)
                .OnDelete(DeleteBehavior.Cascade);

            // 5. Playlist ↔ Song (N:M)
            modelBuilder.Entity<Playlist>()
                .HasMany(p => p.Songs)
                .WithMany(s => s.Playlists)
                .UsingEntity(j => j.ToTable("PlaylistSongs")); // Clean name for the join table
        }
    }
}