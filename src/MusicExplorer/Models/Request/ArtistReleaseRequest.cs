using MediatR;
using MusicExplorer.Models.Response;

namespace MusicExplorer.Models.Request
{
    public class ArtistReleaseRequest : IRequest<ArtistReleaseResponse>
    {
        public Guid ArtistId { get; set; }
    }
}
