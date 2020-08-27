using System.Collections.Generic;

namespace Movies.Api.Models
{
    public class GenreApiResponse
    {
        public IEnumerable<Genre> Genres { get; set; }
    }
}
