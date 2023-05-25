using MediatR;
using MusicExplorer.Models.Request;
using MusicExplorer.Models.Response;
using MusicExplorer.Services;

namespace MusicExplorer.Handlers
{
    public class ArtistSearchHandler : IRequestHandler<ArtistSearchRequest, ArtistSearchResponse>
    {
        private readonly IArtistSearchService _artistSearchService;
        private readonly ILogger<ArtistSearchHandler> _logger;

        public ArtistSearchHandler(IArtistSearchService artistSearchService, ILogger<ArtistSearchHandler> logger)
        {
            _artistSearchService = artistSearchService;
            _logger = logger;
        }

        public async Task<ArtistSearchResponse> Handle(ArtistSearchRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // validate request

                // call artist search service
                var results = await _artistSearchService.GetArtists(request.SearchCriteria);

                if (results.Artists == null)
                {
                    return null;
                }

                return results;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while processing the ArtistSearchRequest.");
                throw;
            }
        }
    }
}
