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
            try
            {
                _logger.LogInformation($"Fetching releases from MusicBrainz.");
                var releases = await _musicBrainzClient.GetReleases(artistId);

                if (releases == null)
                {
                    return null;
                }

                _logger.LogInformation($"Mapping releases to return result.");
                var result = await _mapper.MapArtistRelease(releases);
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while fetching releases.");
                throw;
            }
        }
    }
}
