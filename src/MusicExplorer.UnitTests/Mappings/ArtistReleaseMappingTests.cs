using MusicExplorer.Client.Models;
using MusicExplorer.Mappings;
using MusicExplorer.Models.Response;
using MusicExplorer.UnitTests.Utils;

namespace MusicExplorer.UnitTests.Mappings
{
    public class ArtistReleaseMappingTests
    {
        private readonly MusicBrainzReleasesResponse _actualReleases;
        private readonly ArtistReleaseResponse _expectedResponse;

        public ArtistReleaseMappingTests()
        {
            // Perform the initial setup in the constructor
            _actualReleases = ArtistReleaseDataGenerator.GenerateDummyData();

            _expectedResponse = new ArtistReleaseResponse
            {
                Releases = _actualReleases.Releases.Select(actualRelease => new Models.Response.Release
                {
                    ReleaseId = actualRelease.Id,
                    Title = actualRelease.Title,
                    Status = actualRelease.Status,
                    Label = actualRelease.LabelInfo.Select(labelInfo => new Label
                    {
                        Id = labelInfo.Label?.Id,
                        Name = labelInfo.Label?.Name
                    }).ToList(),
                    NumberOfTracks = actualRelease.Media.Sum(media => media.TrackCount).ToString(),
                    OtherArtists = actualRelease.Media.SelectMany(media => media.Tracks
                        .SelectMany(track => track.ArtistCredit
                            .Select(artistCredit => new OtherArtist
                            {
                                Id = artistCredit.Artist.Id,
                                Name = artistCredit.Artist.Name
                            })))
                            .GroupBy(artist => new { artist.Id, artist.Name })
                            .Select(group => group.First())
                            .ToList()
                }).ToList()
            };
        }

        [Fact]
        public async Task MapArtistRelease_ShouldMapCorrectly()
        {
            // Act
            var sut = new ArtistMapper();
            var result = await sut.MapArtistRelease(_actualReleases);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Releases);

            // Assert the number of releases
            Assert.Equal(_expectedResponse.Releases.Count, result.Releases.Count);

            // Assert each release
            for (int i = 0; i < _expectedResponse.Releases.Count; i++)
            {
                var expectedRelease = _expectedResponse.Releases[i];
                var actualRelease = result.Releases[i];

                Assert.Equal(expectedRelease.ReleaseId, actualRelease.ReleaseId);
                Assert.Equal(expectedRelease.Title, actualRelease.Title);
                Assert.Equal(expectedRelease.Status, actualRelease.Status);

                // Assert the labels
                Assert.NotNull(actualRelease.Label);
                Assert.Equal(expectedRelease.Label.Select(l => l.Id).Distinct().Count(), actualRelease.Label.Count);

                // Assert the number of tracks
                Assert.Equal(expectedRelease.NumberOfTracks, actualRelease.NumberOfTracks);

                // Assert the other artists
                Assert.NotNull(actualRelease.OtherArtists);
                Assert.Equal(expectedRelease.OtherArtists.Select(a => a.Id).Distinct().Count(), actualRelease.OtherArtists.Count);
            }
        }

        [Fact]
        public async Task MapArtistRelease_ShouldHandleNegativeScenario()
        {
            // Arrange
            var emptyReleases = new MusicBrainzReleasesResponse
            {
                Releases = new List<Client.Models.Release>() // Empty list of releases
            };

            // Act
            var sut = new ArtistMapper();
            var result = await sut.MapArtistRelease(emptyReleases);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Releases);
            Assert.Empty(result.Releases);
        }
    }
}
