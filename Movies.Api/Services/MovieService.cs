using Movies.Api.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Movies.Api.Services
{
    public class MovieService : IMovieService
    {
        private const string apiKey = "9b9442509767321935991a03c22e014f";
        private readonly string movieUri = $"https://api.themoviedb.org/3/movie/upcoming?api_key={apiKey}&language=en-US&page=";
        private readonly string genreUri = $"https://api.themoviedb.org/3/genre/movie/list?api_key={apiKey}&language=en-US";


        public async Task<IEnumerable<Movie>> GetUpcoming()
        {
            var http = new HttpClient();

            var genresResponse = await http.GetAsync(genreUri);
            var genresJson = await genresResponse.Content.ReadAsStringAsync();
            var genresApiResponse = JsonConvert.DeserializeObject<GenreApiResponse>(genresJson);

            var apiResponse = new MovieDbApiResponse();
            var apiPage = 1;
            var movies = new List<Movie>();
            do
            {
                var uri = $"{movieUri}{apiPage}";
                var response = await http.GetAsync(uri);
                var json = await response.Content.ReadAsStringAsync();

                apiResponse = JsonConvert.DeserializeObject<MovieDbApiResponse>(json);

                foreach (var movie in apiResponse.Results)
                {
                    foreach (var genreId in movie.GenreIds)
                    {
                        var genre = genresApiResponse.Genres.FirstOrDefault(g => g.Id == genreId);

                        movie.Genres.Add(genre.Name);
                    }

                    movies.Add(movie);
                }

                apiPage++;

            } while (apiPage <= apiResponse.TotalPages);


            return movies;
        }
    }
}