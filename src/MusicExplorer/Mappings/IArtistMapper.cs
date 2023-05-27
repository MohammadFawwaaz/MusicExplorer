using MusicExplorer.Client.Models;
using MusicExplorer.Common.Models.DbContext;
using MusicExplorer.Models.Response;

namespace MusicExplorer.Mappings
{
    public interface IArtistMapper
    {
        Task<ArtistReleaseResponse> MapArtistRelease(MusicBrainzReleasesResponse musicBrainzReleasesResponse);
        Task<ArtistSearchResponse> MapArtist(List<Artist> artists);
    }
}
