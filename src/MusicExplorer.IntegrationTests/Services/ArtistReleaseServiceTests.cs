using Microsoft.Extensions.Logging;
using MusicExplorer.Client;
using MusicExplorer.Client.Models;
using MusicExplorer.Mappings;
using MusicExplorer.Models.Response;
using MusicExplorer.Services;

namespace MusicExplorer.IntegrationTests.Services
{
    public class ArtistReleaseServiceTests : IDisposable
    {
        private readonly Mock<IMusicBrainzClient> _musicBrainzClientMock;
        private readonly Mock<IArtistMapper> _artistMapperMock;
        private readonly Mock<ILogger<ArtistReleaseService>> _loggerMock;
        private readonly ArtistReleaseService _artistReleaseService;

        public ArtistReleaseServiceTests()
        {
            _musicBrainzClientMock = new Mock<IMusicBrainzClient>();
            _artistMapperMock = new Mock<IArtistMapper>();
            _loggerMock = new Mock<ILogger<ArtistReleaseService>>();

            _artistReleaseService = new ArtistReleaseService(
                _musicBrainzClientMock.Object,
                _artistMapperMock.Object,
                _loggerMock.Object);
        }

        public void Dispose()
        {
        }

        [Fact]
        public async Task GetReleases_WithValidArtistId_ReturnsArtistReleaseResponse()
        {
            // Arrange
            Guid artistId = Guid.NewGuid();
            var releases = new List<MusicExplorer.Client.Models.Release>
            {
                new MusicExplorer.Client.Models.Release
                {
                    Title = "Release 1",
                    LabelInfo = new List<LabelInfo>
                    {
                        new LabelInfo
                        {
                            Label = new Miscellaneous
                            {
                                Name = "Label 1"
                            }
                        }
                    },
                    ArtistCredit = new List<ArtistCredit>
                    {
                        new ArtistCredit
                        {
                            Name = "Artist 1"
                        }
                    }
                },
                new MusicExplorer.Client.Models.Release
                {
                    Title = "Release 2",
                    LabelInfo = new List<LabelInfo>
                    {
                        new LabelInfo
                        {
                            Label = new Miscellaneous
                            {
                                Name = "Label 2"
                            }
                        }
                    },
                    ArtistCredit = new List<ArtistCredit>
                    {
                        new ArtistCredit
                        {
                            Name = "Artist 2"
                        }
                    }
                }
            };

            var expectedResponse = new ArtistReleaseResponse
            {
                Releases = new List<Models.Response.Release>
                {
                    new Models.Response.Release
                    {
                        Title = "Release 1",
                        Label = new List<Label>
                        {
                            new Label
                            {
                                Name = "Label 1"
                            }
                        },
                        OtherArtists = new List<OtherArtist>
                        {
                            new OtherArtist
                            {
                                Name = "Artist 1"
                            }
                        }
                    },
                    new Models.Response.Release
                    {
                        Title = "Release 2",
                        Label = new List<Label>
                        {
                            new Label
                            {
                                Name = "Label 2"
                            }
                        },
                        OtherArtists = new List<OtherArtist>
                        {
                            new OtherArtist
                            {
                                Name = "Artist 2"
                            }
                        }
                    }
                }
            };

            _musicBrainzClientMock.Setup(client => client.GetReleases(artistId))
                .ReturnsAsync(new MusicBrainzReleasesResponse { Releases = releases });

            _artistMapperMock.Setup(mapper => mapper.MapArtistRelease(It.IsAny<MusicBrainzReleasesResponse>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var response = await _artistReleaseService.GetReleases(artistId);

            // Assert
            Assert.Equal(expectedResponse, response);
        }

        [Fact]
        public async Task GetReleases_WithNullResponse_ReturnsNull()
        {
            // Arrange
            Guid artistId = Guid.NewGuid();

            _musicBrainzClientMock.Setup(client => client.GetReleases(artistId))
                .ReturnsAsync((MusicBrainzReleasesResponse)null);

            // Act
            var response = await _artistReleaseService.GetReleases(artistId);

            // Assert
            Assert.Null(response);
        }

        [Fact]
        public async Task GetReleases_WithEmptyResponse_ReturnsNull()
        {
            // Arrange
            Guid artistId = Guid.NewGuid();

            _musicBrainzClientMock.Setup(client => client.GetReleases(artistId))
                .ReturnsAsync(new MusicBrainzReleasesResponse { Releases = new List<MusicExplorer.Client.Models.Release>() });

            // Act
            var response = await _artistReleaseService.GetReleases(artistId);

            // Assert
            Assert.Null(response);
        }

        [Fact]
        public async Task GetReleases_WithException_ThrowsException()
        {
            // Arrange
            Guid artistId = Guid.NewGuid();
            var exception = new Exception("Test exception");

            _musicBrainzClientMock.Setup(client => client.GetReleases(artistId))
                .ThrowsAsync(exception);

            // Act & Assert
            await Assert.ThrowsAsync <Exception>(() => _artistReleaseService.GetReleases(artistId));
        }
    }
}
