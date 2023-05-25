using Newtonsoft.Json;

namespace MusicExplorer.Client.Models
{
    public class MusicBrainzReleasesResponse
    {
        [JsonProperty("releases")]
        public List<Release> Releases { get; set; }

        [JsonProperty("release-count")]
        public long ReleaseCount { get; set; }

        [JsonProperty("release-offset")]
        public long ReleaseOffset { get; set; }
    }

    public class Release
    {
        [JsonProperty("text-representation")]
        public TextRepresentation TextRepresentation { get; set; }

        [JsonProperty("packaging-id")]
        public Guid? PackagingId { get; set; }

        [JsonProperty("release-events")]
        public List<ReleaseEvent> ReleaseEvents { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("artist-credit")]
        public List<ArtistCredit> ArtistCredit { get; set; }

        [JsonProperty("status-id")]
        public Guid? StatusId { get; set; }

        [JsonProperty("label-info")]
        public List<LabelInfo> LabelInfo { get; set; }

        [JsonProperty("disambiguation")]
        public string Disambiguation { get; set; }

        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("quality")]
        public string Quality { get; set; }

        [JsonProperty("cover-art-archive")]
        public CoverArtArchive CoverArtArchive { get; set; }

        [JsonProperty("media")]
        public List<Media> Media { get; set; }

        [JsonProperty("barcode")]
        public string Barcode { get; set; }

        [JsonProperty("asin")]
        public string Asin { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("packaging")]
        public string Packaging { get; set; }
    }

    public class ArtistCredit
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("artist")]
        public Miscellaneous Artist { get; set; }

        [JsonProperty("joinphrase")]
        public string Joinphrase { get; set; }
    }

    public class Miscellaneous
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("sort-name")]
        public string SortName { get; set; }

        [JsonProperty("disambiguation")]
        public string Disambiguation { get; set; }

        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("type-id")]
        public Guid? TypeId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("label-code")]
        public long? LabelCode { get; set; }

        [JsonProperty("iso-3166-1-codes")]
        public List<string> Iso31661Codes { get; set; }
    }

    public class CoverArtArchive
    {
        [JsonProperty("front")]
        public bool Front { get; set; }

        [JsonProperty("back")]
        public bool Back { get; set; }

        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("artwork")]
        public bool Artwork { get; set; }

        [JsonProperty("darkened")]
        public bool Darkened { get; set; }
    }

    public class LabelInfo
    {
        [JsonProperty("label")]
        public Miscellaneous Label { get; set; }

        [JsonProperty("catalog-number")]
        public string CatalogNumber { get; set; }
    }

    public class Media
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("format")]
        public string Format { get; set; }

        [JsonProperty("track-offset")]
        public long TrackOffset { get; set; }

        [JsonProperty("position")]
        public long Position { get; set; }

        [JsonProperty("format-id")]
        public Guid FormatId { get; set; }

        [JsonProperty("tracks")]
        public List<Track> Tracks { get; set; }

        [JsonProperty("track-count")]
        public long TrackCount { get; set; }
    }

    public class Track
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("position")]
        public long Position { get; set; }

        [JsonProperty("length")]
        public long? Length { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("artist-credit")]
        public List<ArtistCredit> ArtistCredit { get; set; }

        [JsonProperty("recording")]
        public Recording Recording { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }
    }

    public class Recording
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("first-release-date")]
        public string FirstReleaseDate { get; set; }

        [JsonProperty("disambiguation")]
        public string Disambiguation { get; set; }

        [JsonProperty("length")]
        public long? Length { get; set; }

        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("artist-credit")]
        public List<ArtistCredit> ArtistCredit { get; set; }

        [JsonProperty("video")]
        public bool Video { get; set; }
    }

    public class ReleaseEvent
    {
        [JsonProperty("area")]
        public Miscellaneous Area { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }
    }

    public class TextRepresentation
    {
        [JsonProperty("script")]
        public string Script { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }
    }
}
