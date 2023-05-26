using MusicExplorer.Models;

namespace MusicExplorer.Utils
{
    public static class Helper
    {
        public static PaginationResult<T> PaginateResults<T>(IEnumerable<T> results, int pageNumber, int pageSize)
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

        public static ReleasePaginationResult<T> PaginateReleaseResults<T>(IEnumerable<T> results, int pageNumber, int pageSize)
        {
            var paginationResult = PaginateResults(results, pageNumber, pageSize);

            return new ReleasePaginationResult<T>
            {
                Releases = paginationResult.Results,
                Page = paginationResult.Page,
                PageSize = paginationResult.PageSize,
                NumberOfSearchResults = paginationResult.NumberOfSearchResults,
                NumberOfPages = paginationResult.NumberOfPages
            };
        }
    }
}
