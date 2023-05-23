using Microsoft.AspNetCore.Mvc;
using MusicExplorer.Models;
using MusicExplorer.Models.Request;

namespace MusicExplorer.Controllers
{
    using MediatR;
    using MusicExplorer.Models.Response;

    [ApiController]
    [Route("api/[controller]")]
    public class ArtistController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ArtistController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("search/{searchCriteria}/{pageNumber}/{pageSize}")]
        public async Task<ActionResult<List<ArtistSearchResult>>> SearchArtists(string searchCriteria, int pageNumber, int pageSize)
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

        [HttpGet("{artistId}/releases")]
        public async Task<ActionResult<List<ArtistReleaseResult>>> GetArtistReleases(int artistId)
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

            var query = new ArtistReleasesRequest
            {
                ArtistId = artistId
            };

            var results = await _mediator.Send(query);

            var paginatedResults = PaginateResults(results, pageNumber, pageSize);

            return Ok(paginatedResults);
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
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
        }
    }
}
