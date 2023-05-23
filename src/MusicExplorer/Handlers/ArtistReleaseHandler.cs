using MediatR;
using Microsoft.Extensions.Logging;
using MusicExplorer.Common.Models;
using MusicExplorer.Infrastructure.Infrastructure.Sql;
using MusicExplorer.Models.Request;

namespace MusicExplorer.Handlers
{
    public class ArtistReleaseHandler : IRequestHandler<ArtistReleaseRequest, List<Release>>
    {
        private readonly IArtistRepository _artistRepository;
        private readonly ILogger<ArtistReleaseHandler> _logger;

        public ArtistReleaseHandler(IArtistRepository artistRepository, ILogger<ArtistReleaseHandler> logger)
        {
            _artistRepository = artistRepository;
            _logger = logger;
        }

        public async Task<List<Release>> Handle(ArtistReleaseRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var releases = await _artistRepository.GetArtistsById(request.ArtistId);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while processing the ArtistReleaseRequest.");
                throw;
            }

            return new List<Release>();
        }
    }
}
