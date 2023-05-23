namespace MusicExplorer.Models
{
    public class PaginationResult<T>
    {
        public List<T> Results { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
    }
}
