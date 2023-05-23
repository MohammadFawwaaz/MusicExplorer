using MediatR;
using MusicExplorer.Common.Models;

namespace MusicExplorer.Models.Request
{
    public class ArtistReleaseRequest : IRequest<List<Release>>
    {
        public Guid ArtistId { get; set; }
    }
}
