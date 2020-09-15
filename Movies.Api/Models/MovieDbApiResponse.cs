using Newtonsoft.Json;
using System.Collections.Generic;

namespace Movies.Api.Models
{
    public class MovieDbApiResponse
    {
        public IEnumerable<Movie> Results { get; set; }

        [JsonProperty("total_pages")]
        public int TotalPages { get; set; }
    }
}
