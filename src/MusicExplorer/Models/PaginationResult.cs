namespace MusicExplorer.Models
{
    public class PaginationResult<T>
    {
        public List<T> Results { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int NumberOfSearchResults { get; set; }
        public int NumberOfPages { get; set; }
    }
}
