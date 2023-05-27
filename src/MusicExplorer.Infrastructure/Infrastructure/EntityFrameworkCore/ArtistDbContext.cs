using Microsoft.EntityFrameworkCore;
using MusicExplorer.Common.Models.DbContext;

namespace MusicExplorer.Infrastructure.Infrastructure.EntityFrameworkCore
{
    public class ArtistDbContext : DbContext
    {
        public DbSet<Artist> Artist { get; set; }

        public ArtistDbContext(DbContextOptions<ArtistDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}
