using MusicExplorer.Client.Models;

namespace MusicExplorer.Client
{
    public interface IMusicBrainzClient
    {
        Task<MusicBrainzReleasesResponse> GetReleases(Guid artistId);
    }
}
