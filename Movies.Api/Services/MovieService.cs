using Movies.Api.Domain;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Movies.Api.Services
{
    public class MovieService : IMovieService
    {
        public async Task<IEnumerable<Movie>> GetUpcoming()
        {
            var http = new HttpClient();
            var apiKey = "9b9442509767321935991a03c22e014f";
            var apiPage = 1;
            var uri = $"https://api.themoviedb.org/3/movie/upcoming?api_key={apiKey}&language=en-US&page={apiPage}";

            var response = await http.GetAsync(uri);

            var json = await response.Content.ReadAsStringAsync();

            return null;
        }
    }
}
