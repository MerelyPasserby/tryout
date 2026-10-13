using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using tryout.DTOs.Game;
using tryout.DTOs.Genre;
using tryout.Services;

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

        [HttpGet("games/{id:int:min(1)}")]
        public async Task<ActionResult<GameResponseDto>> GetGame([FromRoute] int id)
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

        [HttpPut("games/{id:int:min(1)}")]
        public async Task<ActionResult<GameResponseDto>> UpdateGame(
            [FromRoute] int id,
            [FromBody] GameUpdateDto dto)
        {
            var game = await _service.UpdateGameAsync(id, dto);

            if (game is null)
                return NotFound();

            return Ok(game);
        }

        [HttpDelete("games/{id:int:min(1)}")]
        public async Task<IActionResult> DeleteGame([FromRoute] int id)
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

        [HttpGet("genres/{id:int:min(1)}")]
        public async Task<ActionResult<GenreResponseDto>> GetGenre([FromRoute] int id)
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

        [HttpPut("genres/{id:int:min(1)}")]
        public async Task<ActionResult<GenreResponseDto>> UpdateGenre(
            [FromRoute] int id,
            [FromBody] GenreUpdateDto dto)
        {
            var genre = await _service.UpdateGenreAsync(id, dto);

            if (genre is null)
                return NotFound();

            return Ok(genre);
        }
        
        [HttpDelete("genres/{id:int:min(1)}")]
        public async Task<IActionResult> DeleteGenre([FromRoute] int id)
        {
            var deleted = await _service.DeleteGenreAsync(id);

            if (!deleted)
                return NotFound();

            return Ok();
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("error")]
        public IActionResult HandleError()
        {
            return Problem(
                statusCode: StatusCodes.Status500InternalServerError,
                title: "An unexpected error occurred.");
        }
    }
}
