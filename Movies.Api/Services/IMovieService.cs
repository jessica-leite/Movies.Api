﻿using Movies.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Api.Services
{
    public interface IMovieService
    {
        Task<IEnumerable<Movie>> GetUpcoming();
    }
}
