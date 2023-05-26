namespace MusicExplorer.Models.Request
{
    public class ArtistSearchRequest : IHttpRequest
    {
        public string SearchCriteria { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
