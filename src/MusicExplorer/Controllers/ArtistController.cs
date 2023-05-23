using Microsoft.AspNetCore.Mvc;
using MusicExplorer.Models;
using MusicExplorer.Models.Request;
using MediatR;
using MusicExplorer.Models.Response;
using System.Net;

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

        [HttpGet("search/{searchCriteria}/{pageNumber}/{pageSize}")]
        public async Task<ActionResult<List<ArtistSearchResponse>>> SearchArtists(string searchCriteria, int pageNumber, int pageSize)
        {
            try
            {
                var query = new ArtistSearchRequest
                {
                    SearchCriteria = searchCriteria,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };

                var results = await _mediator.Send(query);

                var paginatedResults = PaginateResults(results, pageNumber, pageSize);

                return Ok(paginatedResults);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while searching artists.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{artistId}/releases")]
        public async Task<ActionResult<List<ArtistReleaseResponse>>> GetArtistReleases(Guid artistId)
        {
            try
            {
                int pageNumber;
                int pageSize;

                if (Request.Headers.ContainsKey("pageNumber") && Request.Headers.ContainsKey("pageSize"))
                {
                    pageNumber = int.Parse(Request.Headers["pageNumber"].ToString());
                    pageSize = int.Parse(Request.Headers["pageSize"].ToString());
                }
                else
                {
                    // Set default values
                    pageNumber = 1;
                    pageSize = 10;
                }

                var query = new ArtistReleaseRequest
                {
                    ArtistId = artistId
                };

                var results = await _mediator.Send(query);

                var paginatedResults = PaginateResults(results, pageNumber, pageSize);

                return Ok(paginatedResults);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while searching for artist.");
                return StatusCode(500, "Internal server error");
            }
        }

        private static PaginationResult<T> PaginateResults<T>(IEnumerable<T> results, int pageNumber, int pageSize)
        {
            var totalCount = results.Count();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var paginatedResults = results
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PaginationResult<T>
            {
                Results = paginatedResults,
                Page = pageNumber,
                PageSize = pageSize,
                NumberOfSearchResults = totalCount,
                NumberOfPages = totalPages
            };
        }
    }
}
