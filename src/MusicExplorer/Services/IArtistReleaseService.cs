using MusicExplorer.Models.Response;

namespace MusicExplorer.Services
{
    public interface IArtistReleaseService
    {
        Task<ArtistReleaseResponse> GetReleases(Guid artistId);
    }
}
