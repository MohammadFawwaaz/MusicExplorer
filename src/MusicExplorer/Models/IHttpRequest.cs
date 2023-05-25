using MediatR;

namespace MusicExplorer.Models
{
    public interface IHttpRequest : IRequest<IResult>
    {
    }
}
