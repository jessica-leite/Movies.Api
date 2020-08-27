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

            var apiPage = 1;
            var page1Uri = $"{movieUri}{apiPage}";

            // first request to find out how many others (totalPages) will be needed in asynchronous mode.
            var response = await _http.GetAsync(page1Uri);
            var json = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<MovieDbApiResponse>(json);

            var movies = new List<Movie>();

            // add items from page 1
            var page1Movies = FillGenres(apiResponse.Results, genres).ToList();
            movies.AddRange(page1Movies);

            var pages = new List<int>();
            for (int i = ++apiPage; i <= apiResponse.TotalPages; i++)
            {
                pages.Add(i);
            }
            var allTasks = pages.Select(page => _http.GetAsync($"{movieUri}{page}"));
            var allResponses = await Task.WhenAll(allTasks);

            // add items from the remaining pages
            foreach (var res in allResponses)
            {
                var jsonResult = await res.Content.ReadAsStringAsync();
                var apiRes = JsonConvert.DeserializeObject<MovieDbApiResponse>(jsonResult);

                var remainingMovies = FillGenres(apiRes.Results, genres);
                movies.AddRange(remainingMovies);
            }

            return movies;
        }

        private async Task<IList<Genre>> GetGenres()
        {
            var response = await _http.GetAsync(genreUri);
            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<GenreApiResponse>(json).Genres;
        }

        private IEnumerable<Movie> FillGenres(IEnumerable<Movie> movies, IList<Genre> genres)
        {
            foreach (var movie in movies)
            {
                foreach (var genreId in movie.GenreIds)
                {
                    var genre = genres.FirstOrDefault(g => g.Id == genreId);
                    movie.Genres.Add(genre.Name);
                }
            }
            return movies;
        }
    }
}