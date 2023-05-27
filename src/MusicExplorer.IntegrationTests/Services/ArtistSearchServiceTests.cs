using Microsoft.Extensions.Logging;
using MusicExplorer.Common.Models.DbContext;
using MusicExplorer.Infrastructure.Infrastructure.Sql;
using MusicExplorer.Mappings;
using MusicExplorer.Models.Response;
using MusicExplorer.Services;

namespace MusicExplorer.IntegrationTests.Services
{
    public class ArtistSearchServiceTests : IDisposable
    {
        private readonly Mock<IArtistRepository> _artistRepositoryMock;
        private readonly Mock<IArtistMapper> _artistMapperMock;
        private readonly Mock<ILogger<ArtistSearchService>> _loggerMock;
        private readonly ArtistSearchService _artistSearchService;

        public ArtistSearchServiceTests()
        {
            _artistRepositoryMock = new Mock<IArtistRepository>();
            _artistMapperMock = new Mock<IArtistMapper>();
            _loggerMock = new Mock<ILogger<ArtistSearchService>>();

            _artistSearchService = new ArtistSearchService(
                _artistRepositoryMock.Object,
                _artistMapperMock.Object,
                _loggerMock.Object);
        }

        public void Dispose()
        {
        }

        [Fact]
        public async Task GetArtists_WithValidSearchCriteria_ReturnsArtistSearchResponse()
        {
            // Arrange
            string searchCriteria = "joh";
            var artists = new List<Artist>
            {
                new Artist
                {
                    Id = 1,
                    Name = "John Caltron",
                    UniqueIdentifier = Guid.NewGuid(),
                    Country = "US",
                    Aliases = "Alias 1, Alias 2"
                },
                new Artist
                {
                    Id = 2,
                    Name = "Johny xyz",
                    UniqueIdentifier = Guid.NewGuid(),
                    Country = "GB",
                    Aliases = "Alias 3, Alias 4"
                }
            };

            var expectedResponse = new ArtistSearchResponse
            {
                Results = new List<Common.Models.Artist>
                {
                    new Common.Models.Artist
                    {
                        Name = "John Caltron",
                        Country = "US",
                        Alias = new List<string> { "Alias 1", "Alias 2" }
                    },
                    new Common.Models.Artist
                    {
                        Name = "Johny xyz",
                        Country = "GB",
                        Alias = new List<string> { "Alias 3", "Alias 4" }
                    }
                }
            };

            _artistRepositoryMock.Setup(repo => repo.GetArtistsByName(searchCriteria))
                .ReturnsAsync(artists);

            _artistMapperMock.Setup(mapper => mapper.MapArtist(artists))
                .ReturnsAsync(expectedResponse);

            // Act
            var response = await _artistSearchService.GetArtists(searchCriteria);

            // Assert
            Assert.Equal(expectedResponse, response);
        }

        [Fact]
        public async Task GetArtists_WithEmptySearchCriteria_ReturnsNull()
        {
            // Arrange
            string searchCriteria = "";
            var artists = new List<Artist>();

            _artistRepositoryMock.Setup(repo => repo.GetArtistsByName(searchCriteria))
                .ReturnsAsync(artists);

            // Act
            var response = await _artistSearchService.GetArtists(searchCriteria);

            // Assert
            Assert.Null(response);
        }

        [Fact]
        public async Task GetArtists_WithException_ThrowsException()
        {
            // Arrange
            string searchCriteria = "SomeArtist";
            var exception = new Exception("Test exception");

            _artistRepositoryMock.Setup(repo => repo.GetArtistsByName(searchCriteria))
                .ThrowsAsync(exception);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _artistSearchService.GetArtists(searchCriteria));
        }
    }
}
