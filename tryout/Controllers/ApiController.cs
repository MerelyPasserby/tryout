using Microsoft.AspNetCore.Mvc;
using tryout.DTOs.Game;
using tryout.DTOs.Genre;
using tryout.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace tryout.Controllers
{
    [Route("api")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly DbService _service;

        public ApiController(DbService service)
        {
            _service = service;
        }

        [HttpGet("games")]
        public async Task<ActionResult<List<GameResponseDto>>> GetGames()
        {
            var games = await _service.GetGamesAsync();

            return Ok(games);
        }

        [HttpGet("games/{id}")]
        public async Task<ActionResult<GameResponseDto>> GetGame(int id)
        {
            var game = await _service.GetGameByIdAsync(id);

            if (game is null)
                return NotFound();

            return Ok(game);
        }

        [HttpPost("games")]
        public async Task<ActionResult<GameResponseDto>> CreateGame(
            [FromBody] GameCreateDto dto)
        {
            try
            {
                var game = await _service.CreateGameAsync(dto);

                return Ok(game);
            }
            catch (InvalidOperationException)
            {
                return StatusCode(500, "Game was created, but could not be retrieved.");
            }
        }

        [HttpPut("games/{id}")]
        public async Task<ActionResult<GameResponseDto>> UpdateGame(
            int id,
            [FromBody] GameUpdateDto dto)
        {
            var game = await _service.UpdateGameAsync(id, dto);

            if (game is null)
                return NotFound();

            return Ok(game);
        }

        [HttpDelete("games/{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            var deleted = await _service.DeleteGameAsync(id);

            if (!deleted)
                return NotFound();

            return Ok();
        }

        [HttpGet("genres")]
        public async Task<ActionResult<List<GenreResponseDto>>> GetGenres()
        {
            var genres = await _service.GetGenresAsync();

            return Ok(genres);
        }

        [HttpGet("genres/{id}")]
        public async Task<ActionResult<GenreResponseDto>> GetGenre(int id)
        {
            var genre = await _service.GetGenreByIdAsync(id);

            if (genre is null)
                return NotFound();

            return Ok(genre);
        }

        [HttpPost("genres")]
        public async Task<ActionResult<GenreResponseDto>> CreateGenre(
            [FromBody] GenreCreateDto dto)
        {
            var genre = await _service.CreateGenreAsync(dto);

            return Ok(genre);
        }

        [HttpPut("genres/{id}")]
        public async Task<ActionResult<GenreResponseDto>> UpdateGenre(
            int id,
            [FromBody] GenreUpdateDto dto)
        {
            var genre = await _service.UpdateGenreAsync(id, dto);

            if (genre is null)
                return NotFound();

            return Ok(genre);
        }
        
        [HttpDelete("genres/{id}")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            var deleted = await _service.DeleteGenreAsync(id);

            if (!deleted)
                return NotFound();

            return Ok();
        }
    }
}
