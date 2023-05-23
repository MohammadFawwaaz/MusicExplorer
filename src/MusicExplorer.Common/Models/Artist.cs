﻿using Newtonsoft.Json;

namespace MusicExplorer.Common.Models
{
    public class Artist
    {
        [JsonProperty("name")]
        public string ArtistName { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("alias")]
        public List<string> Aliases { get; set; }
    }
}
