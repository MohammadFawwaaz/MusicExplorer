using MusicExplorer.Common.Models;
using Newtonsoft.Json;

namespace MusicExplorer.Models.Response
{
    public class ArtistReleaseResponse
    {
        [JsonProperty("releases")]
        public List<Release> Releases { get; set; }
    }
}
