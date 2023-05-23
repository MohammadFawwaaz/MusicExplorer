using Newtonsoft.Json;

namespace MusicExplorer.Common.Models
{
    public class Release
    {
        [JsonProperty("releaseId")]
        public Guid ReleaseId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("numberOfTracks")]
        public string NumberOfTracks { get; set; }

        [JsonProperty("otherArtists")]
        public List<OtherArtist> OtherArtists { get; set; }
    }

    public class OtherArtist
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
