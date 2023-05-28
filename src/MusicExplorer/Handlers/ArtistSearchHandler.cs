using FluentValidation;
using MediatR;
using MusicExplorer.Models.Request;
using MusicExplorer.Services;

namespace MusicExplorer.Handlers
{
    public class ArtistSearchHandler : IRequestHandler<ArtistSearchRequest, IResult>
    {
        private readonly IValidator<ArtistSearchRequest> _validator;
        private readonly IArtistSearchService _artistSearchService;
        private readonly ILogger<ArtistSearchHandler> _logger;

        public ArtistSearchHandler(IValidator<ArtistSearchRequest> validator,
            IArtistSearchService artistSearchService, 
            ILogger<ArtistSearchHandler> logger)
        {
            _validator = validator;
            _artistSearchService = artistSearchService;
            _logger = logger;
        }

        public async Task<IResult> Handle(ArtistSearchRequest request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Validating Search Request.");
                var validationResult = _validator.ValidateAsync(request, cancellationToken).Result;

                if (!validationResult.IsValid)
                {
                    return Results.BadRequest(validationResult.Errors);
                }

                _logger.LogInformation($"Getting artists.");
                var results = await _artistSearchService.GetArtists(request.SearchCriteria);

                if (results == null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(results);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while processing the ArtistSearchRequest.");
                throw;
            }
        }
    }
}
