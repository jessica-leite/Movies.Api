using Microsoft.AspNetCore.Mvc;
using Movies.Api.Services;

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

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
