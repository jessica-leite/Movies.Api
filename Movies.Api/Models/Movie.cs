using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Movies.Api.Models
{
    public class Movie
    {
        public Movie()
        {
            Genres = new List<string>();
        }

        public string Title { get; set; }

        [JsonProperty("genre_ids")]
        public IList<int> GenreIds { get; set; }
        public IList<string> Genres { get; set; }

        [JsonProperty("release_date")]
        public DateTime ReleaseDate { get; set; }
    }
}
