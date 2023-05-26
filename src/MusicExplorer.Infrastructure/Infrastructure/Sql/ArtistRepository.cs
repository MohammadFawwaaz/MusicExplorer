using Microsoft.EntityFrameworkCore;
using MusicExplorer.Common.Models.DbContext;
using MusicExplorer.Infrastructure.Infrastructure.EntityFrameworkCore;

namespace MusicExplorer.Infrastructure.Infrastructure.Sql
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly ArtistDbContext _dbContext;

        public ArtistRepository(ArtistDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<Artist>> GetArtistsById(Guid artistId)
        {
            return _dbContext.Artist
                .Where(a => a.UniqueIdentifier == artistId)
                .ToListAsync();
        }

        public Task<List<Artist>> GetArtistsByName(string artistName)
        {
            var lowerArtistName = artistName.ToLower();

            var artists = _dbContext.Artist
                .Where(a =>
                    EF.Functions.Like(a.Name, lowerArtistName + "%") ||
                    (a.Aliases != null && EF.Functions.Like(a.Aliases, "%" + lowerArtistName + "%")))
                .ToList();

            return Task.FromResult(artists);
        }
    }
}
