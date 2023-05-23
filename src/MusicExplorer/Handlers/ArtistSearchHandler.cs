using MediatR;
using MusicExplorer.Infrastructure.Infrastructure.Sql;
using MusicExplorer.Models.Request;
using MusicExplorer.Models.Response;
using System;

namespace MusicExplorer.Handlers
{
    public class ArtistSearchHandler : IRequestHandler<ArtistSearchRequest, List<ArtistSearchResponse>>
    {
        private readonly IArtistRepository _artistRepository;
        private readonly ILogger<ArtistSearchHandler> _logger;

        public ArtistSearchHandler(IArtistRepository artistRepository, ILogger<ArtistSearchHandler> logger)
        {
            _artistRepository = artistRepository;
            _logger = logger;
        }

        public async Task<List<ArtistSearchResponse>> Handle(ArtistSearchRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var artistResults = await _artistRepository.GetArtistsByName(request.SearchCriteria);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while processing the ArtistSearchRequest.");
                throw;
            }

            return new List<ArtistSearchResponse>();
        }
    }
}
