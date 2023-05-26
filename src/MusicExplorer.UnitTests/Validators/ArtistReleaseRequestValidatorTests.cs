using MusicExplorer.Models.Request;
using MusicExplorer.Validators;

namespace MusicExplorer.UnitTests.Validators
{
    public class ArtistReleaseRequestValidatorTests
    {
        private readonly ArtistReleaseRequestValidator _validator;

        public ArtistReleaseRequestValidatorTests()
        {
            _validator = new ArtistReleaseRequestValidator();
        }

        [Fact]
        public void Validate_ValidArtistId_ShouldPass()
        {
            // Arrange
            var request = new ArtistReleaseRequest
            {
                ArtistId = Guid.NewGuid(),
                PageNumber = 1,
                PageSize = 10
            };

            // Act
            var result = _validator.Validate(request);

            // Assert
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData("00000000-0000-0000-0000-000000000000")]
        [InlineData("11111111-1111-1111-1111-111111111111")]
        [InlineData("22222222-2222-2222-2222-222222222222")]
        [InlineData("99999999-9999-9999-9999-999999999999")]
        public void Validate_InvalidArtistId_ShouldFail(string artistId)
        {
            // Arrange
            var request = new ArtistReleaseRequest
            {
                ArtistId = new Guid(artistId),
                PageNumber = 1,
                PageSize = 10
            };

            // Act
            var result = _validator.Validate(request);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, error => error.PropertyName == "ArtistId");
            Assert.Contains(result.Errors, error => error.ErrorMessage == "ArtistId must be a valid Guid separated by '-' and cannot be all 0, 1, 2, ..., or 9");
        }

        [Fact]
        public void Validate_ValidPageNumber_ShouldPass()
        {
            // Arrange
            var request = new ArtistReleaseRequest
            {
                ArtistId = Guid.NewGuid(),
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
            var request = new ArtistReleaseRequest
            {
                ArtistId = Guid.NewGuid(),
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
            var request = new ArtistReleaseRequest
            {
                ArtistId = Guid.NewGuid(),
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
            var request = new ArtistReleaseRequest
            {
                ArtistId = Guid.NewGuid(),
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
