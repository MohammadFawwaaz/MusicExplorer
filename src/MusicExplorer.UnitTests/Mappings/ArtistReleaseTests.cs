using Bogus;
using MusicExplorer.Client.Models;
using MusicExplorer.Mappings;
using MusicExplorer.Models.Response;
using MusicExplorer.UnitTests.Utils;

namespace MusicExplorer.UnitTests.Mappings
{
    public class ArtistReleaseTests
    {
        [Fact]
        public async Task MapArtistRelease_ShouldMapCorrectly()
        {
            // Arrange
            var releases = ArtistReleaseMusicBrainz.GenerateDummyData();

            var actualReleases = new MusicBrainzReleasesResponse
            {
                Releases = releases.Releases
            };

            var faker = new Faker();
            var expectedReleases = faker.Make(1, () => new Models.Response.Release
            {
                ReleaseId = faker.Random.Guid(),
                Title = faker.Lorem.Word(),
                Status = faker.Lorem.Word(),
                Label = faker.Make(2, () => new Label
                {
                    Id = faker.Random.Guid(),
                    Name = faker.Company.CompanyName()
                }).ToList(),
                NumberOfTracks = faker.Random.Number(1, 10).ToString(),
                OtherArtists = faker.Make(2, () => new OtherArtist
                {
                    Id = faker.Random.Guid(),
                    Name = faker.Name.FullName()
                }).ToList()
            }).ToList();

            var expectedResponse = new ArtistReleaseResponse
            {
                Releases = expectedReleases
            };

            // Act
            var sut = new ArtistMapper();
            var result = await sut.MapArtistRelease(actualReleases);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Releases);

            // Assert the number of releases
            Assert.Equal(expectedResponse.Releases.Count, result.Releases.Count);

            // Assert each release
            for (int i = 0; i < expectedResponse.Releases.Count; i++)
            {
                var expectedRelease = expectedResponse.Releases[i];
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
    }
}
