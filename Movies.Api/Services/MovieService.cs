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
        private readonly HttpClient _http;

        public MovieService()
        {
            _http = new HttpClient();
        }

        public async Task<IEnumerable<Movie>> GetUpcoming()
        {
            var genres = await GetGenres();
            var apiResponse = new MovieDbApiResponse();
            var apiPage = 1;
            var movies = new List<Movie>();
            do
            {
                var uri = $"{movieUri}{apiPage}";
                var response = await _http.GetAsync(uri);
                var json = await response.Content.ReadAsStringAsync();

                apiResponse = JsonConvert.DeserializeObject<MovieDbApiResponse>(json);

                foreach (var movie in apiResponse.Results)
                {
                    FillGenres(movie, genres);
                    movies.Add(movie);
                }
                apiPage++;

            } while (apiPage <= apiResponse.TotalPages);

            return movies;
        }

        private async Task<IList<Genre>> GetGenres()
        {
            var genresResponse = await _http.GetAsync(genreUri);
            var genresJson = await genresResponse.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<GenreApiResponse>(genresJson);

            return response.Genres;
        }

        private void FillGenres(Movie movie, IList<Genre> genres)
        {
            foreach (var genreId in movie.GenreIds)
            {
                var genre = genres.FirstOrDefault(g => g.Id == genreId);
                movie.Genres.Add(genre.Name);
            }
        }
    }
}