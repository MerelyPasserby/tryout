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
        public DbService(AppDbContext context) => _context = context;

        public async Task<List<GameResponseDto>> GetGamesAsync()
        {
            return await _context.Games
                .Include(g => g.Genre)
                .Select(g => new GameResponseDto
                {
                    Id = g.Id,
                    Title = g.Title,
                    Description = g.Description,
                    ReleaseYear = g.ReleaseYear,
                    Rating = g.Rating,
                    CreatedAt = g.CreatedAt,
                    EditedAt = g.EditedAt,
                    Genre = new GenreResponseDto
                    {
                        Id = g.Genre.Id,
                        Name = g.Genre.Name,
                        Description = g.Genre.Description
                    }
                })
                .ToListAsync();
        }

        public async Task<GameResponseDto?> GetGameByIdAsync(int id)
        {
            return await _context.Games
                .Include(g => g.Genre)
                .Where(g => g.Id == id)
                .Select(g => new GameResponseDto
                {
                    Id = g.Id,
                    Title = g.Title,
                    Description = g.Description,
                    ReleaseYear = g.ReleaseYear,
                    Rating = g.Rating,
                    CreatedAt = g.CreatedAt,
                    EditedAt = g.EditedAt,
                    Genre = new GenreResponseDto
                    {
                        Id = g.Genre.Id,
                        Name = g.Genre.Name,
                        Description = g.Genre.Description
                    }
                })
                .FirstOrDefaultAsync();
        }

        public async Task<GameResponseDto> CreateGameAsync(GameCreateDto dto)
        {
            var game = new Game
            {
                Title = dto.Title,
                Description = dto.Description,
                ReleaseYear = dto.ReleaseYear,
                GenreId = dto.GenreId,
                Rating = 0,
                CreatedAt = DateTime.UtcNow,
                EditedAt = DateTime.UtcNow
            };

            _context.Games.Add(game);
            await _context.SaveChangesAsync();

            return await GetGameByIdAsync(game.Id)
                ?? throw new InvalidOperationException("Game was not found after creation.");
        }

        public async Task<GameResponseDto?> UpdateGameAsync(int id, GameUpdateDto dto)
        {
            var game = await _context.Games.FindAsync(id);

            if (game is null)
                return null;

            game.Title = dto.Title;
            game.Description = dto.Description;
            game.ReleaseYear = dto.ReleaseYear;
            game.GenreId = dto.GenreId;
            game.EditedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return await GetGameByIdAsync(id);
        }

        public async Task<bool> DeleteGameAsync(int id)
        {
            var game = await _context.Games.FindAsync(id);

            if (game is null)
                return false;

            _context.Games.Remove(game);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<GenreResponseDto>> GetGenresAsync()
        {
            return await _context.Genres
                .Select(g => new GenreResponseDto
                {
                    Id = g.Id,
                    Name = g.Name,
                    Description = g.Description
                })
                .ToListAsync();
        }

        public async Task<GenreResponseDto?> GetGenreByIdAsync(int id)
        {
            return await _context.Genres
                .Where(g => g.Id == id)
                .Select(g => new GenreResponseDto
                {
                    Id = g.Id,
                    Name = g.Name,
                    Description = g.Description
                })
                .FirstOrDefaultAsync();
        }

        public async Task<GenreResponseDto> CreateGenreAsync(GenreCreateDto dto)
        {
            var genre = new Genre
            {
                Name = dto.Name,
                Description = dto.Description
            };

            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();

            return new GenreResponseDto
            {
                Id = genre.Id,
                Name = genre.Name,
                Description = genre.Description
            };
        }

        public async Task<GenreResponseDto?> UpdateGenreAsync(int id, GenreUpdateDto dto)
        {
            var genre = await _context.Genres.FindAsync(id);

            if (genre is null)
                return null;

            genre.Name = dto.Name;
            genre.Description = dto.Description;

            await _context.SaveChangesAsync();

            return await GetGenreByIdAsync(id);
        }

        public async Task<bool> DeleteGenreAsync(int id)
        {
            var genre = await _context.Genres.FindAsync(id);

            if (genre is null)
                return false;

            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}