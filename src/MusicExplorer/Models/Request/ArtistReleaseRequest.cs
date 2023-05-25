namespace MusicExplorer.Models.Request
{
    public class ArtistReleaseRequest : IHttpRequest
    {
        public Guid ArtistId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
