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
                .NotNull().WithMessage("SearchCriteria cannot be null")
                .NotEmpty().WithMessage("SearchCriteria cannot be empty")
                .Must(searchCriteria => !string.IsNullOrWhiteSpace(searchCriteria))
                .WithMessage("SearchCriteria cannot be null, empty, or whitespace")
                .MinimumLength(3).WithMessage("SearchCriteria must be of length 3 or more")
                .Must(searchCriteria => !Regex.IsMatch(searchCriteria, @"\d"))
                .WithMessage("SearchCriteria cannot contain numbers");

            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1)
                .WithMessage("PageNumber must be greater than or equal to 1");

            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1)
                .WithMessage("PageSize must be greater than or equal to 1");
        }
    }
}
