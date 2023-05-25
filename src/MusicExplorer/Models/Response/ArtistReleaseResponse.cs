using Newtonsoft.Json;

namespace MusicExplorer.Models.Response
{
    public class ArtistReleaseResponse
    {
        [JsonProperty("releases")]
        public List<Release> Releases { get; set; }
    }

    public class Release
    {
        [JsonProperty("releaseId")]
        public Guid ReleaseId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("label")]
        public List<Label> Label { get; set; }

        [JsonProperty("numberOfTracks")]
        public string NumberOfTracks { get; set; }

        [JsonProperty("otherArtists")]
        public List<OtherArtist> OtherArtists { get; set; }
    }

    public class Label
    {
        [JsonProperty("id")]
        public Guid? Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class OtherArtist
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
