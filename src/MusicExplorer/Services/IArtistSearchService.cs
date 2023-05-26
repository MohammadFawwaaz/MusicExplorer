using MusicExplorer.Models.Response;

namespace MusicExplorer.Services
{
    public interface IArtistSearchService
    {
        Task<ArtistSearchResponse> GetArtists(string searchCriteria);
    }
}
