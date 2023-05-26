using MusicExplorer.Common.Models;
using Newtonsoft.Json;

namespace MusicExplorer.Models.Response
{
    public class ArtistSearchResponse
    {
        [JsonProperty("results")]
        public List<Artist> Results { get; set; }
    }
}
