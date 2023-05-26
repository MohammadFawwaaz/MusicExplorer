using MusicExplorer.Common.Models.DbContext;
using MusicExplorer.Mappings;
using MusicExplorer.Models.Response;
using MusicExplorer.UnitTests.Utils;

namespace MusicExplorer.UnitTests.Mappings
{
    public class ArtistSearchMappingTests
    {
        private readonly List<Artist> _artists;
        private readonly ArtistSearchResponse _expectedResponse;

        public ArtistSearchMappingTests()
        {
            // Perform the initial setup in the constructor
            _artists = ArtistSearchDataGenerator.GenerateDummyData();

            _expectedResponse = new ArtistSearchResponse
            {
                Results = _artists.Select(item => new Common.Models.Artist
                {
                    Name = item.Name,
                    Country = item.Country,
                    Alias = item.Aliases?.Split(',').ToList() ?? new List<string>()
                }).ToList()
            };
        }

        [Fact]
        public async Task MapArtist_ShouldMapCorrectly()
        {
            // Arrange
            var sut = new ArtistMapper();

            // Act
            var result = await sut.MapArtist(_artists);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Results);

            // Assert the number of results
            Assert.Equal(_expectedResponse.Results.Count, result.Results.Count);

            // Assert each artist
            for (int i = 0; i < _expectedResponse.Results.Count; i++)
            {
                var expectedArtist = _expectedResponse.Results[i];
                var actualArtist = result.Results[i];

                Assert.Equal(expectedArtist.Name, actualArtist.Name);
                Assert.Equal(expectedArtist.Country, actualArtist.Country);

                // Assert the aliases
                Assert.NotNull(actualArtist.Alias);
                Assert.Equal(expectedArtist.Alias.Count, actualArtist.Alias.Count);
                Assert.Equal(expectedArtist.Alias, actualArtist.Alias);
            }
        }

        [Fact]
        public async Task MapArtist_ShouldHandleEmptyList()
        {
            // Arrange
            var emptyList = new List<Artist>();

            // Act
            var sut = new ArtistMapper();
            var result = await sut.MapArtist(emptyList);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Results);
            Assert.Empty(result.Results);
        }
    }
}
