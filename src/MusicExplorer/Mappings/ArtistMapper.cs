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
            var listRelease = musicBrainzReleasesResponse.Releases.Select(item => new Models.Response.Release
            {
                ReleaseId = item.Id,
                Title = item.Title,
                Status = item.Status,
                Label = item.LabelInfo.Select(itemLabel => new Label
                {
                    Id = itemLabel.Label?.Id,
                    Name = itemLabel.Label?.Name
                }).ToList(),
                NumberOfTracks = item.Media?.FirstOrDefault()?.TrackCount.ToString(),
                OtherArtists = item.Media?.FirstOrDefault()?.Tracks
                .SelectMany(itemTrack => itemTrack.ArtistCredit.Select(itemArtistCredit => new OtherArtist
                {
                    Id = itemArtistCredit.Artist.Id,
                    Name = itemArtistCredit.Artist.Name
                }))
                .GroupBy(artist => new { artist.Id, artist.Name })
                .Select(group => group.First())
                .ToList()
            }).ToList();

            var response = new ArtistReleaseResponse { Releases = listRelease };
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
