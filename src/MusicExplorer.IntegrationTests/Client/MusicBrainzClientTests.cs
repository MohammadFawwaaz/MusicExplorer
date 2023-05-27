using Microsoft.Extensions.Configuration;
using MusicExplorer.Client;

namespace MusicExplorer.IntegrationTests.Client
{
    public class MusicBrainzClientTests
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly IMusicBrainzClient _musicBrainzClient;

        public MusicBrainzClientTests()
        {
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(_configuration.GetSection("Clients:MusicBrainz:BaseUri").Value)
            };

            _httpClient.DefaultRequestHeaders.Add("User-Agent", "MusicExplorerTest/1.0");
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            // Create an instance of the MusicBrainzClient
            _musicBrainzClient = new MusicBrainzClient(_httpClient);
        }

        [Fact]
        public async Task GetReleases_WithValidArtistId_ReturnsReleases()
        {
            // Arrange
            Guid artistId = Guid.Parse("435f1441-0f43-479d-92db-a506449a686b");

            // Act
            var releases = await _musicBrainzClient.GetReleases(artistId);

            // Assert
            Assert.NotNull(releases);
            Assert.NotEmpty(releases.Releases);
        }

        [Fact]
        public async Task GetReleases_WithInvalidArtistId_ReturnsNull()
        {
            // Arrange
            Guid artistId = Guid.NewGuid();

            // Act
            var releases = await _musicBrainzClient.GetReleases(artistId);

            // Assert
            Assert.Null(releases);
        }
    }
}
