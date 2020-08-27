using System;
using System.Collections.Generic;

namespace Movies.Api.Domain
{
    public class Movie
    {
        public string Name { get; set; }
        public IEnumerable<string> Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
