using FluentValidation;
using MusicExplorer.Models.Request;

namespace MusicExplorer.Validators
{
    public class ArtistReleaseRequestValidator : AbstractValidator<ArtistReleaseRequest>
    {
        public ArtistReleaseRequestValidator()
        {
            RuleFor(x => x.ArtistId)
                .Must(artistId => Guid.TryParse(artistId.ToString(), out _))
                .WithMessage("ArtistId must be a valid Guid separated by '-'");

            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1)
                .WithMessage("PageNumber must be greater than or equal to 1");

            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1)
                .WithMessage("PageSize must be greater than or equal to 1");
        }
    }
}
