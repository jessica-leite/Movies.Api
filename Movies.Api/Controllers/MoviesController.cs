using Microsoft.AspNetCore.Mvc;
using Movies.Api.Services;
using System.Threading.Tasks;

namespace Movies.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _service;

        public MoviesController(IMovieService service)
        {
            _service = service;
        }

        [HttpGet("upcoming-releases")]
        public async Task<IActionResult> GetUpcoming()
        {
            var movies = await _service.GetUpcoming();

            return Ok(movies);
        }
    }
}
