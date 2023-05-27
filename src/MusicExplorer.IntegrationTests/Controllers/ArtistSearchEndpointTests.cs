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
    public class ArtistSearchEndpointTests
    {
        private readonly ArtistController _artistController;
        private readonly Mock<ILogger<ArtistController>> _loggerMock;
        private readonly Mock<IMediator> _mediatorMock;

        public ArtistSearchEndpointTests()
        {
            _loggerMock = new Mock<ILogger<ArtistController>>();
            _mediatorMock = new Mock<IMediator>();

            _artistController = new ArtistController(_loggerMock.Object, _mediatorMock.Object);
        }

        [Fact]
        public async Task SearchArtists_WithValidParameters_ReturnsOkResult()
        {
            // Arrange
            string searchCriteria = "John";
            int pageNumber = 1;
            int pageSize = 10;

            // Configure the mediator mock to return a valid response
            _mediatorMock.Setup(m => m.Send(It.IsAny<ArtistSearchRequest>(), default))
                .ReturnsAsync(Results.Ok(new ArtistSearchResponse()));

            // Act
            var response = await _artistController.SearchArtists(searchCriteria, pageNumber, pageSize) as ObjectResult;

            // Assert
            Assert.NotNull(response);
            Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
        }

        [Fact]
        public async Task SearchArtists_WithBadRequest_ReturnsBadRequestResult()
        {
            // Arrange
            string searchCriteria = "Invalid";
            int pageNumber = 0;
            int pageSize = 10;

            // Configure the mediator mock to return a BadRequest response
            var validationFailures = new List<ValidationFailure>
            {
                new ValidationFailure("pageNumber", "Invalid..")
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<ArtistSearchRequest>(), default))
                .ReturnsAsync(Results.BadRequest(validationFailures));

            // Act
            var response = await _artistController.SearchArtists(searchCriteria, pageNumber, pageSize) as ObjectResult;

            // Assert
            Assert.NotNull(response);
            Assert.Equal(StatusCodes.Status400BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task SearchArtists_WithNotFound_ReturnsNotFoundResult()
        {
            // Arrange
            string searchCriteria = "Nonexistent";
            int pageNumber = 1;
            int pageSize = 10;

            // Configure the mediator mock to return a NotFound response
            _mediatorMock.Setup(m => m.Send(It.IsAny<ArtistSearchRequest>(), default))
                .ReturnsAsync(Results.NotFound());

            // Act
            var response = await _artistController.SearchArtists(searchCriteria, pageNumber, pageSize) as StatusCodeResult;

            // Assert
            Assert.NotNull(response);
            Assert.Equal(StatusCodes.Status404NotFound, response.StatusCode);
        }

        [Fact]
        public async Task SearchArtists_WithUnknownObjectResult_ReturnsInternalServerErrorResult()
        {
            // Arrange
            string searchCriteria = "Error";
            int pageNumber = 1;
            int pageSize = 10;

            // Configure the mediator mock to return a result with an unknown object
            var unknownObject = new List<string>();

            _mediatorMock.Setup(m => m.Send(It.IsAny<ArtistSearchRequest>(), default))
                .ReturnsAsync(Results.Ok(unknownObject));

            // Act
            var response = await _artistController.SearchArtists(searchCriteria, pageNumber, pageSize) as ObjectResult;

            // Assert
            Assert.NotNull(response);
            Assert.Equal(StatusCodes.Status500InternalServerError, response.StatusCode);
        }
    }
}
