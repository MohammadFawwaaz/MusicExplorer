using MusicExplorer.Client.Models;
using Newtonsoft.Json;

namespace MusicExplorer.Client
{
    public class MusicBrainzClient : IMusicBrainzClient
    {
        private readonly HttpClient _httpClient;

        public MusicBrainzClient(HttpClient httpClient)
        {

            _httpClient = httpClient;

        }

        public async Task<MusicBrainzReleasesResponse> GetReleases(Guid artistId)
        {
            var response = await _httpClient.GetAsync($"release?artist={artistId}&inc=recordings+labels+artist-credits");

            if(response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<MusicBrainzReleasesResponse>(content);
        }
    }
}
