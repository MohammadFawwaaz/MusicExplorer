using MusicExplorer.Client;
using MusicExplorer.Mappings;
using MusicExplorer.Models.Response;

namespace MusicExplorer.Services
{
    public class ArtistReleaseService : IArtistReleaseService
    {
        private readonly IMusicBrainzClient _musicBrainzClient;
        private readonly IArtistMapper _mapper;
        private readonly ILogger<ArtistReleaseService> _logger;

        public ArtistReleaseService(IMusicBrainzClient musicBrainzClient,
            IArtistMapper mapper,
            ILogger<ArtistReleaseService> logger)
        {
            _musicBrainzClient = musicBrainzClient;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ArtistReleaseResponse> GetReleases(Guid artistId)
        {
            var releases = await _musicBrainzClient.GetReleases(artistId);

            if (releases == null)
            {
                return null;
            }

            var result = await _mapper.MapArtistRelease(releases);
            return result;
        }
    }
}
