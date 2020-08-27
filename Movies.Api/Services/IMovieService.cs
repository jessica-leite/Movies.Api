using Movies.Api.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Api.Services
{
    public interface IMovieService
    {
        Task<IEnumerable<Movie>> GetUpcoming();
    }
}
