using MusicExplorer.Common.Models.DbContext;

namespace MusicExplorer.Infrastructure.Infrastructure.Sql
{
    public interface IArtistRepository
    {
        Task<List<Artist>> GetArtistsById(Guid artistId);
        Task<List<Artist>> GetArtistsByName(string artistName);
    }
}
