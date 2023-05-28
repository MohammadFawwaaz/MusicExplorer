using FluentValidation;
using MediatR;
using MusicExplorer.Models.Request;
using MusicExplorer.Services;

namespace MusicExplorer.Handlers
{
    public class ArtistReleaseHandler : IRequestHandler<ArtistReleaseRequest, IResult>
    {
        private readonly IValidator<ArtistReleaseRequest> _validator;
        private readonly IArtistReleaseService _artistReleaseService;
        private readonly ILogger<ArtistReleaseHandler> _logger;

        public ArtistReleaseHandler(IValidator<ArtistReleaseRequest> validator,
            IArtistReleaseService artistReleaseService, 
            ILogger<ArtistReleaseHandler> logger)
        {
            _validator = validator;
            _artistReleaseService = artistReleaseService;
            _logger = logger;
        }

        public async Task<IResult> Handle(ArtistReleaseRequest request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Validating Release Request");
                var validationResult = _validator.ValidateAsync(request, cancellationToken).Result;

                if (!validationResult.IsValid)
                {
                    return Results.BadRequest(validationResult.Errors);
                }

                _logger.LogInformation($"Getting releases.");
                var results = await _artistReleaseService.GetReleases(request.ArtistId);

                if (results == null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(results);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while processing the ArtistReleaseRequest.");
                throw;
            }
        }
    }
}
