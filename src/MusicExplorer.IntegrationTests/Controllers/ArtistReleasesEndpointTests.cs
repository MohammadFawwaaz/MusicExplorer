using AutoFixture;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MusicExplorer.Controllers;
using MusicExplorer.Models.Request;
using MusicExplorer.Models.Response;

namespace MusicExplorer.IntegrationTests.Controllers
{
    public class ArtistReleasesEndpointTests
    {
        private readonly ArtistController _artistController;
        private readonly Mock<ILogger<ArtistController>> _loggerMock;
        private readonly Mock<IMediator> _mediatorMock;

        public ArtistReleasesEndpointTests()
        {
            _loggerMock = new Mock<ILogger<ArtistController>>();
            _mediatorMock = new Mock<IMediator>();

            // Create an instance of the ArtistController with the mocked mediator
            _artistController = new ArtistController(_loggerMock.Object, _mediatorMock.Object);
        }

        [Fact]
        public async Task GetArtistReleases_WithValidArtistId_ReturnsOkResult()
        {
            // Arrange
            var artistId = Guid.NewGuid();
            var pageNumber = 1;
            var pageSize = 10;
            var expectedReleases = CreateDummyReleases(pageSize);

            // Configure the mediator mock to return a valid response
            _mediatorMock.Setup(m => m.Send(It.IsAny<ArtistReleaseRequest>(), default))
                .ReturnsAsync(Results.Ok(new ArtistReleaseResponse { Releases = expectedReleases }));

            // Set the request headers
            _artistController.ControllerContext.HttpContext = new DefaultHttpContext();
            _artistController.ControllerContext.HttpContext.Request.Headers["X-PageNumber"] = pageNumber.ToString();
            _artistController.ControllerContext.HttpContext.Request.Headers["X-PageSize"] = pageSize.ToString();

            // Act
            var response = await _artistController.GetArtistReleases(artistId) as ObjectResult;

            // Assert
            Assert.NotNull(response);
            Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
        }

        [Fact]
        public async Task GetArtistReleases_WithBadRequest_ReturnsBadRequestResult()
        {
            // Arrange
            var artistId = Guid.NewGuid();
            var pageNumber = 1;
            var pageSize = 10;

            // Configure the mediator mock to return a BadRequest response
            var validationFailures = new List<ValidationFailure>
            {
                new ValidationFailure("Field", "Validation error")
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<ArtistReleaseRequest>(), default))
                .ReturnsAsync(Results.BadRequest<List<ValidationFailure>>(validationFailures));

            // Set the request headers
            _artistController.ControllerContext.HttpContext = new DefaultHttpContext();
            _artistController.ControllerContext.HttpContext.Request.Headers["X-PageNumber"] = pageNumber.ToString();
            _artistController.ControllerContext.HttpContext.Request.Headers["X-PageSize"] = pageSize.ToString();

            // Act
            var response = await _artistController.GetArtistReleases(artistId) as ObjectResult;

            // Assert
            Assert.NotNull(response);
            Assert.Equal(StatusCodes.Status400BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GetArtistReleases_WithNotFound_ReturnsNotFoundResult()
        {
            // Arrange
            var artistId = Guid.NewGuid();
            var pageNumber = 1;
            var pageSize = 10;

            // Configure the mediator mock to return a NotFound response
            _mediatorMock.Setup(m => m.Send(It.IsAny<ArtistReleaseRequest>(), default))
                .ReturnsAsync(Results.NotFound());

            // Set the request headers
            _artistController.ControllerContext.HttpContext = new DefaultHttpContext();
            _artistController.ControllerContext.HttpContext.Request.Headers["X-PageNumber"] = pageNumber.ToString();
            _artistController.ControllerContext.HttpContext.Request.Headers["X-PageSize"] = pageSize.ToString();

            // Act
            var response = await _artistController.GetArtistReleases(artistId) as StatusCodeResult;

            // Assert
            Assert.NotNull(response);
            Assert.Equal(StatusCodes.Status404NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetArtistReleases_WithUnknownObject_ReturnsInternalServerErrorResult()
        {
            // Arrange
            var artistId = Guid.NewGuid();
            var pageNumber = 1;
            var pageSize = 10;

            // Configure the mediator mock to throw an exception
            var unknownObject = new List<int>();

            _mediatorMock.Setup(m => m.Send(It.IsAny<ArtistReleaseRequest>(), default))
                .ReturnsAsync(Results.Ok(unknownObject));

            // Set the request headers
            _artistController.ControllerContext.HttpContext = new DefaultHttpContext();
            _artistController.ControllerContext.HttpContext.Request.Headers["X-PageNumber"] = pageNumber.ToString();
            _artistController.ControllerContext.HttpContext.Request.Headers["X-PageSize"] = pageSize.ToString();

            // Act
            var response = await _artistController.GetArtistReleases(artistId) as ObjectResult;

            // Assert
            Assert.NotNull(response);
            Assert.Equal(StatusCodes.Status500InternalServerError, response.StatusCode);
        }

        private List<Release> CreateDummyReleases(int count)
        {
            var fixture = new Fixture();
            return fixture.CreateMany<Release>(count).ToList();
        }
    }
}
