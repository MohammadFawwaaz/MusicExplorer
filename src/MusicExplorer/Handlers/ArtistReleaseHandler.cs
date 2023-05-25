using MediatR;
using MusicExplorer.Models.Request;
using MusicExplorer.Models.Response;
using MusicExplorer.Services;

namespace MusicExplorer.Handlers
{
    public class ArtistReleaseHandler : IRequestHandler<ArtistReleaseRequest, ArtistReleaseResponse>
    {
        private readonly IArtistReleaseService _artistReleaseService;
        private readonly ILogger<ArtistReleaseHandler> _logger;

        public ArtistReleaseHandler(IArtistReleaseService artistReleaseService, ILogger<ArtistReleaseHandler> logger)
        {
            _artistReleaseService = artistReleaseService;
            _logger = logger;
        }

        public async Task<ArtistReleaseResponse> Handle(ArtistReleaseRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // validate request


                var results = await _artistReleaseService.GetReleases(request.ArtistId);

                if (results.Releases == null)
                {
                    return null;
                }

                return results;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while processing the ArtistReleaseRequest.");
                throw;
            }
        }
    }
}
