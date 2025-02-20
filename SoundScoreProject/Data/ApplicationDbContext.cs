using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SoundScore.Data.Entities;

namespace SoundScore.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
          : base(options)
        {
        }

        public DbSet<Album> Albums { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships and constraints here if needed

            // Example:
            modelBuilder.Entity<Album>()
              .HasOne(a => a.Artist)
              .WithMany(a => a.Albums)
              .HasForeignKey(a => a.ArtistId);

            modelBuilder.Entity<Track>()
              .HasOne(t => t.Album)
              .WithMany(a => a.Tracks)
              .HasForeignKey(t => t.AlbumId);
        }
    }
}