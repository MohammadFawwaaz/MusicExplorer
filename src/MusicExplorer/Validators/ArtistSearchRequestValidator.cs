using FluentValidation;
using MusicExplorer.Models.Request;
using System.Text.RegularExpressions;

namespace MusicExplorer.Validators
{
    public class ArtistSearchRequestValidator : AbstractValidator<ArtistSearchRequest>
    {
        public ArtistSearchRequestValidator()
        {
            RuleFor(x => x.SearchCriteria)
                .Must(searchCriteria => searchCriteria == null || !string.IsNullOrWhiteSpace(searchCriteria))
                .WithMessage("SearchCriteria cannot be null, empty, or whitespace")
                .MinimumLength(3).WithMessage("SearchCriteria must be of length 3 or more")
                .Must(searchCriteria => !ContainNumbers(searchCriteria))
                .WithMessage("SearchCriteria cannot contain numbers");

            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1)
                .WithMessage("PageNumber must be greater than or equal to 1");

            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1)
                .WithMessage("PageSize must be greater than or equal to 1");
        }

        private static bool ContainNumbers(string value) => value != null && value.Any(char.IsDigit);
    }
}
