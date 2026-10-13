using AutoMapper;
using Microsoft.EntityFrameworkCore;
using tryout.Data;
using tryout.DTOs.Game;
using tryout.DTOs.Genre;
using tryout.Models;

namespace tryout.Services
{
    public class DbService
    {
        readonly AppDbContext _context;
        readonly IMapper _mapper;
        readonly ILogger<DbService> _logger;
        public DbService(AppDbContext context, IMapper mapper, ILogger<DbService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<GameResponseDto>> GetGamesAsync()
        {
            var games = await _context.Games.Include(g => g.Genre).ToListAsync();

            if (games.Count == 0)
                return null;

            var gamesDtos = games.Select(g => _mapper.Map<GameResponseDto>(g)).ToList();

            return gamesDtos;
        }

        public async Task<GameResponseDto?> GetGameByIdAsync(int id)
        {
            var game = await _context.Games.Include(g => g.Genre).Where(g => g.Id == id).FirstAsync();

            if (game == null)
                return null;

            var gameDto = _mapper.Map<GameResponseDto>(game);

            return gameDto;
        }

        public async Task<GameResponseDto> CreateGameAsync(GameCreateDto dto)
        {
            try
            {
                var game = _mapper.Map<Game>(dto);

                game.CreatedAt = DateTime.UtcNow;
                game.EditedAt = DateTime.UtcNow;
                game.Rating = 0;


                _context.Games.Add(game);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Game created. GameId: {GameId}, Title: {Title}", game.Id, game.Title);

                return await GetGameByIdAsync(game.Id)
                    ?? throw new InvalidOperationException("Game was not found after creation.");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "CreateGame Error. Title: {Title}", dto.Title);

                throw;
            }
            
        }

        public async Task<GameResponseDto?> UpdateGameAsync(int id, GameUpdateDto dto)
        {
            try
            {
                var game = await _context.Games.FindAsync(id);

                if (game is null)
                    return null;

                _mapper.Map(dto, game);

                game.EditedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                _logger.LogInformation("Game updated. GameId: {GameId}, Title: {Title}", game.Id, game.Title);

                return await GetGameByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UpdateGame Error. Title: {Title}", dto.Title);

                throw;
            }
            
        }

        public async Task<bool> DeleteGameAsync(int id)
        {
            try
            {
                var game = await _context.Games.FindAsync(id);

                if (game is null)
                    return false;

                _context.Games.Remove(game);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Game deleted. GameId: {GameId}, Title: {Title}", game.Id, game.Title);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DeleteGame Error. GameId: {GameId}", id);

                throw;
            }
            
        }

        public async Task<List<GenreResponseDto>> GetGenresAsync()
        {
            var genres = await _context.Genres.ToListAsync();

            if(genres.Count == 0)
            {
                return null;
            }

            var genresDtos = genres.Select(g => _mapper.Map<GenreResponseDto>(g)).ToList();

            return genresDtos;
        }

        public async Task<GenreResponseDto?> GetGenreByIdAsync(int id)
        {
            var genre = await _context.Genres.Where(g => g.Id == id).FirstAsync();

            if (genre == null)
                return null;

            var genreDto = _mapper.Map<GenreResponseDto>(genre);

            return genreDto;
        }

        public async Task<GenreResponseDto> CreateGenreAsync(GenreCreateDto dto)
        {
            try
            {
                var genre = _mapper.Map<Genre>(dto);

                _context.Genres.Add(genre);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Genre created. GenreId: {GenreId}, Name: {Name}", genre.Id, genre.Name);

                return await GetGenreByIdAsync(genre.Id)
                    ?? throw new InvalidOperationException("Genre was not found after cretaion");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CreateGenre Error. Name: {Name}", dto.Name);

                throw;
            }          
        }

        public async Task<GenreResponseDto?> UpdateGenreAsync(int id, GenreUpdateDto dto)
        {
            try
            {
                var genre = await _context.Genres.FindAsync(id);

                if (genre is null)
                    return null;

                _mapper.Map(dto, genre);

                await _context.SaveChangesAsync();

                _logger.LogInformation("Genre updated. GenreId: {GenreId}, Name: {Name}", genre.Id, genre.Name);

                return await GetGenreByIdAsync(id);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "UpdateGenre Error. Name: {Name}", dto.Name);

                throw;
            }
            
        }

        public async Task<bool> DeleteGenreAsync(int id)
        {
            try
            {
                var genre = await _context.Genres.FindAsync(id);

                if (genre is null)
                    return false;

                _context.Genres.Remove(genre);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Genre deleted. GenreId: {GenreId}, Name: {Name}", genre.Id, genre.Name);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DeleteGenre Error. GenreId: {GenreId}", id);

                throw;
            }
            
        }
    }
}