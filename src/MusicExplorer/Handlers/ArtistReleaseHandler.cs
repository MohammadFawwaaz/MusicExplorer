using MediatR;
using MusicExplorer.Models.Request;
using MusicExplorer.Models.Response;
using MusicExplorer.Services;

namespace MusicExplorer.Handlers
{
    public class ArtistReleaseHandler : IRequestHandler<ArtistReleaseRequest, ArtistReleaseResponse>
    {
        private readonly IArtistReleaseService _artistService;
        private readonly ILogger<ArtistReleaseHandler> _logger;

        public ArtistReleaseHandler(IArtistReleaseService artistService, ILogger<ArtistReleaseHandler> logger)
        {
            _artistService = artistService;
            _logger = logger;
        }

        public async Task<ArtistReleaseResponse> Handle(ArtistReleaseRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // validate request


                // call artist service
                var results = await _artistService.GetReleases(request.ArtistId);

                if (results == null)
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
