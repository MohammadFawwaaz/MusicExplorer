using FluentValidation;
using MusicExplorer.Models.Request;

namespace MusicExplorer.Validators
{
    public class ArtistReleaseRequestValidator : AbstractValidator<ArtistReleaseRequest>
    {
        public ArtistReleaseRequestValidator()
        {
            RuleFor(x => x.ArtistId)
                .Must(artistId =>
                {
                    if (Guid.TryParse(artistId.ToString(), out var guid))
                    {
                        var bytes = guid.ToByteArray();
                        var firstByte = bytes[0];
                        for (int i = 1; i < bytes.Length; i++)
                        {
                            if (bytes[i] != firstByte)
                            {
                                return true;
                            }
                        }
                    }
                    return false;
                })
                .WithMessage("ArtistId must be a valid Guid separated by '-' and cannot be all 0, 1, 2, ..., or 9");

            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1)
                .WithMessage("PageNumber must be greater than or equal to 1");

            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1)
                .WithMessage("PageSize must be greater than or equal to 1");
        }
    }
}
