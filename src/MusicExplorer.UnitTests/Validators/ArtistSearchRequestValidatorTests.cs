using MusicExplorer.Models.Request;
using MusicExplorer.Validators;

namespace MusicExplorer.UnitTests.Validators
{
    public class ArtistSearchRequestValidatorTests
    {
        private readonly ArtistSearchRequestValidator _validator;

        public ArtistSearchRequestValidatorTests()
        {
            _validator = new ArtistSearchRequestValidator();
        }

        [Fact]
        public void Validate_ValidSearchCriteria_ShouldPass()
        {
            // Arrange
            var request = new ArtistSearchRequest
            {
                SearchCriteria = "John",
                PageNumber = 1,
                PageSize = 10
            };

            // Act
            var result = _validator.Validate(request);

            // Assert
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void Validate_NullOrEmptySearchCriteria_ShouldFail(string searchCriteria)
        {
            // Arrange
            var request = new ArtistSearchRequest
            {
                SearchCriteria = searchCriteria,
                PageNumber = 1,
                PageSize = 10
            };

            // Act
            var result = _validator.Validate(request);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, error => error.PropertyName == "SearchCriteria");
            Assert.Contains(result.Errors, error => error.ErrorMessage == "SearchCriteria cannot be null, empty, or whitespace");
        }

        [Theory]
        [InlineData("ab")]
        [InlineData("12")]
        public void Validate_InvalidSearchCriteriaLength_ShouldFail(string searchCriteria)
        {
            // Arrange
            var request = new ArtistSearchRequest
            {
                SearchCriteria = searchCriteria,
                PageNumber = 1,
                PageSize = 10
            };

            // Act
            var result = _validator.Validate(request);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, error => error.PropertyName == "SearchCriteria");
            Assert.Contains(result.Errors, error => error.ErrorMessage == "SearchCriteria must be of length 3 or more");
        }

        [Theory]
        [InlineData("John1")]
        [InlineData("123")]
        public void Validate_InvalidSearchCriteriaContainingNumbers_ShouldFail(string searchCriteria)
        {
            // Arrange
            var request = new ArtistSearchRequest
            {
                SearchCriteria = searchCriteria,
                PageNumber = 1,
                PageSize = 10
            };

            // Act
            var result = _validator.Validate(request);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, error => error.PropertyName == "SearchCriteria");
            Assert.Contains(result.Errors, error => error.ErrorMessage == "SearchCriteria cannot contain numbers");
        }

        [Fact]
        public void Validate_ValidPageNumber_ShouldPass()
        {
            // Arrange
            var request = new ArtistSearchRequest
            {
                SearchCriteria = "John",
                PageNumber = 1,
                PageSize = 10
            };

            // Act
            var result = _validator.Validate(request);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void Validate_InvalidPageNumber_ShouldFail()
        {
            // Arrange
            var request = new ArtistSearchRequest
            {
                SearchCriteria = "John",
                PageNumber = 0,
                PageSize = 10
            };

            // Act
            var result = _validator.Validate(request);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, error => error.PropertyName == "PageNumber");
            Assert.Contains(result.Errors, error => error.ErrorMessage == "PageNumber must be greater than or equal to 1");
        }

        [Fact]
        public void Validate_ValidPageSize_ShouldPass()
        {
            // Arrange
            var request = new ArtistSearchRequest
            {
                SearchCriteria = "John",
                PageNumber = 1,
                PageSize = 10
            };

            // Act
            var result = _validator.Validate(request);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void Validate_InvalidPageSize_ShouldFail()
        {
            // Arrange
            var request = new ArtistSearchRequest
            {
                SearchCriteria = "John",
                PageNumber = 1,
                PageSize = 0
            };

            // Act
            var result = _validator.Validate(request);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, error => error.PropertyName == "PageSize");
            Assert.Contains(result.Errors, error => error.ErrorMessage == "PageSize must be greater than or equal to 1");
        }
    }
}
