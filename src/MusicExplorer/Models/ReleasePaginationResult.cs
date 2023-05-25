namespace MusicExplorer.Models
{
    public class ReleasePaginationResult<T>
    {
        public List<T> Releases { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int NumberOfSearchResults { get; set; }
        public int NumberOfPages { get; set; }
    }
}
