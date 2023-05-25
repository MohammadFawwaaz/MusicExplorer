using MusicExplorer.Infrastructure.Infrastructure.Sql;
using MusicExplorer.Mappings;
using MusicExplorer.Models.Response;

namespace MusicExplorer.Services
{
    public class ArtistSearchService : IArtistSearchService
    {
        private readonly IArtistRepository _artistRepository;
        private readonly IArtistMapper _mapper;
        private readonly ILogger<ArtistSearchService> _logger;

        public ArtistSearchService(IArtistRepository artistRepository,
            IArtistMapper mapper,
            ILogger<ArtistSearchService> logger)
        {
            _artistRepository = artistRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ArtistSearchResponse> GetArtists(string searchCriteria)
        {
            var artists = await _artistRepository.GetArtistsByName(searchCriteria);

            if (artists == null || artists?.Count == 0)
            {
                return null;
            }

            var result = await _mapper.MapArtist(artists);
            return result;
        }
    }
}
