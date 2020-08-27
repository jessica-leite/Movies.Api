using Movies.Api.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Movies.Api.Services
{
    public class MovieService : IMovieService
    {
        private const string apiKey = "9b9442509767321935991a03c22e014f";
        private readonly string baseUri = $"https://api.themoviedb.org/3/movie/upcoming?api_key={apiKey}&language=en-US&page=";

        public async Task<IEnumerable<Movie>> GetUpcoming()
        {
            var http = new HttpClient();
            var apiResponse = new MovieDbApiResponse();

            var apiPage = 1;
            var movies = new List<Movie>();
            do
            {
                var uri = $"{baseUri}{apiPage}";
                var response = await http.GetAsync(uri);
                var json = await response.Content.ReadAsStringAsync();

                apiResponse = JsonConvert.DeserializeObject<MovieDbApiResponse>(json);
                movies.AddRange(apiResponse.Results);

                apiPage++;

            } while (apiPage <= apiResponse.TotalPages);

            return movies;
        }
    }
}
