using Microsoft.AspNetCore.Mvc;
using MusicExplorer.Models.Request;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using MusicExplorer.Models.Response;
using MusicExplorer.Utils;
using FluentValidation.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace MusicExplorer.Controllers
{
    [ApiController]
    [Route("artist")]
    public class ArtistController : ControllerBase
    {
        private readonly ILogger<ArtistController> _logger;
        private readonly IMediator _mediator;

        public ArtistController(ILogger<ArtistController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieve a list of Artists by search criteria
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("search/{searchCriteria}/{pageNumber}/{pageSize}")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<ArtistSearchResponse>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SearchArtists(string searchCriteria, int pageNumber, int pageSize)
        {
            try
            {
                var query = new ArtistSearchRequest
                {
                    SearchCriteria = searchCriteria,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };

                var result = await _mediator.Send(query);

                if (result is BadRequest<List<ValidationFailure>> validationFailures)
                {
                    return BadRequest(validationFailures.Value.Select(x => x.ErrorMessage));
                }

                if (result is NotFound)
                {
                    return NotFound();
                }

                if (result is Ok<ArtistSearchResponse> paginatedResult)
                {
                    var paginatedResults = Helper.PaginateResults(paginatedResult.Value.Results, pageNumber, pageSize);
                    return Ok(paginatedResults);
                }

                return StatusCode(500, "Internal server error");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while searching artists.");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Retrieve a list of Releases by Artist MBID
        /// </summary>
        /// <param name="artistId"></param>
        /// <returns></returns>
        [HttpGet("{artistId}/releases")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<ArtistReleaseResponse>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetArtistReleases(Guid artistId)
        {
            try
            {
                int pageNumber = 0;
                int pageSize = 0;

                if (Request.Headers.ContainsKey("X-PageNumber") && Request.Headers.ContainsKey("X-PageSize"))
                {
                    if (int.TryParse(Request.Headers["X-PageNumber"].ToString(), out int parsedPageNumber))
                    {
                        pageNumber = parsedPageNumber;
                    }

                    if (int.TryParse(Request.Headers["X-PageSize"].ToString(), out int parsedPageSize))
                    {
                        pageSize = parsedPageSize;
                    }
                }

                var query = new ArtistReleaseRequest
                {
                    ArtistId = artistId,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };

                var result = await _mediator.Send(query);

                if (result is BadRequest<List<ValidationFailure>> validationFailures)
                {
                    return BadRequest(validationFailures.Value.Select(x => x.ErrorMessage));
                }

                if (result is NotFound)
                {
                    return NotFound();
                }

                if (result is Ok<ArtistReleaseResponse> paginatedResult)
                {
                    var paginatedResults = Helper.PaginateReleaseResults(paginatedResult.Value.Releases, pageNumber, pageSize);
                    return Ok(paginatedResults);
                }

                return StatusCode(500, "Internal server error");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while searching for artist releases.");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
