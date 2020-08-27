using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Movies.Api.Models
{
    public class Movie
    {
        public string Title { get; set; }

        [JsonProperty("genre_ids")]
        public IEnumerable<int> GenreId { get; set; }
        public IEnumerable<string> Genres { get; set; }

        [JsonProperty("release_date")]
        public DateTime ReleaseDate { get; set; }
    }
}
