using Microsoft.EntityFrameworkCore;
using MiniSpotify.Models;

namespace MiniSpotify.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Entidades existentes
        public DbSet<User> Users => Set<User>();
        public DbSet<Book> Books => Set<Book>(); 
        public DbSet<Artist> Artists => Set<Artist>();
        public DbSet<ArtistDetail> ArtistDetails => Set<ArtistDetail>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>();
            modelBuilder.Entity<Book>();

            modelBuilder.Entity<Artist>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);

                entity.HasOne(a => a.ArtistDetail)
                      .WithOne(ad => ad.Artist)
                      .HasForeignKey<ArtistDetail>(ad => ad.ArtistId)
                      .OnDelete(DeleteBehavior.Cascade); 
            });

            modelBuilder.Entity<ArtistDetail>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd(); 
            });
        }
    }
}