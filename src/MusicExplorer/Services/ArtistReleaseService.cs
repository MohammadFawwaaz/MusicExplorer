using MusicExplorer.Client;
using MusicExplorer.Models.Response;

namespace MusicExplorer.Services
{
    public class ArtistReleaseService : IArtistReleaseService
    {
        private readonly IMusicBrainzClient _musicBrainzClient;
        private readonly ILogger<ArtistReleaseService> _logger;

        public ArtistReleaseService(IMusicBrainzClient musicBrainzClient, ILogger<ArtistReleaseService> logger)
        {
            _musicBrainzClient = musicBrainzClient;
            _logger = logger;
        }

        public async Task<ArtistReleaseResponse> GetReleases(Guid artistId)
        {
            var releases = await _musicBrainzClient.GetReleases(artistId);

            if (releases == null)
            {
                return null;
            }

            // map MusicBrainzReleaseResponse to ArtistReleaseResponse and return
            return new ArtistReleaseResponse();
        }
    }
}
