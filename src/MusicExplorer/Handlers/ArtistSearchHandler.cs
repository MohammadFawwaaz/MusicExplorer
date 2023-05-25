using MediatR;
using MusicExplorer.Common.Models;
using MusicExplorer.Infrastructure.Infrastructure.Sql;
using MusicExplorer.Models.Request;
using MusicExplorer.Models.Response;

namespace MusicExplorer.Handlers
{
    public class ArtistSearchHandler : IRequestHandler<ArtistSearchRequest, ArtistSearchResponse>
    {
        private readonly IArtistRepository _artistRepository;
        private readonly ILogger<ArtistSearchHandler> _logger;

        public ArtistSearchHandler(IArtistRepository artistRepository, ILogger<ArtistSearchHandler> logger)
        {
            _artistRepository = artistRepository;
            _logger = logger;
        }

        public async Task<ArtistSearchResponse> Handle(ArtistSearchRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // validate request

                // call artist search service
                var results = await _artistRepository.GetArtistsByName(request.SearchCriteria);

                if (results == null)
                {
                    return new ArtistSearchResponse();
                }

                var artists = results.Select(item => new Artist
                {
                    ArtistName = item.Name,
                    Country = item.Country,
                    Aliases = item.Aliases?.Split(',').ToList() ?? new List<string>()
                }).ToList();

                return new ArtistSearchResponse { Artists = artists };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while processing the ArtistSearchRequest.");
                throw;
            }
        }
    }
}
