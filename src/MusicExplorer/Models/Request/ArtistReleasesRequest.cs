using MediatR;

namespace MusicExplorer.Models.Request
{
    public class ArtistReleasesRequest : IRequest<List<Release>>
    {
        public int ArtistId { get; set; }
    }
}
