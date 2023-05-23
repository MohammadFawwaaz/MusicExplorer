using MediatR;
using MusicExplorer.Common.Models;
using MusicExplorer.Infrastructure.Infrastructure.Sql;
using MusicExplorer.Models.Request;
using MusicExplorer.Models.Response;

namespace MusicExplorer.Handlers
{
    public class ArtistReleaseHandler : IRequestHandler<ArtistReleaseRequest, ArtistReleaseResponse>
    {
        private readonly IArtistRepository _artistRepository;
        private readonly ILogger<ArtistReleaseHandler> _logger;

        public ArtistReleaseHandler(IArtistRepository artistRepository, ILogger<ArtistReleaseHandler> logger)
        {
            _artistRepository = artistRepository;
            _logger = logger;
        }

        public async Task<ArtistReleaseResponse> Handle(ArtistReleaseRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var results = await _artistRepository.GetArtistsById(request.ArtistId);

                if (results == null)
                {
                    return new ArtistReleaseResponse();
                }

                var releases = results.Select(item => new Release
                {
                    ReleaseId = Guid.NewGuid()
                }).ToList();

                return new ArtistReleaseResponse { Releases = releases };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while processing the ArtistReleaseRequest.");
                throw;
            }
        }
    }
}
