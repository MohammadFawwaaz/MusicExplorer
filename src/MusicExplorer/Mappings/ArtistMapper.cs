using MusicExplorer.Client.Models;
using MusicExplorer.Common.Models.DbContext;
using MusicExplorer.Models.Response;

namespace MusicExplorer.Mappings
{
    public class ArtistMapper : IArtistMapper
    {
        public ArtistMapper()
        {
            
        }

        public Task<ArtistReleaseResponse> MapArtistRelease(MusicBrainzReleasesResponse musicBrainzReleasesResponse)
        {
            // TODO: implement mappings
            var result = new ArtistReleaseResponse().Releases;

            var response = new ArtistReleaseResponse { Releases = result };
            return Task.FromResult(response);
        }

        public Task<ArtistSearchResponse> MapArtist(List<Artist> artists)
        {
            var result = artists.Select(item => new Common.Models.Artist
            {
                ArtistName = item.Name,
                Country = item.Country,
                Aliases = item.Aliases?.Split(',').Select(x => x.Trim()).ToList() ?? new List<string>()
            }).ToList();

            var response = new ArtistSearchResponse { Artists = result };
            return Task.FromResult(response);
        }
    }
}
