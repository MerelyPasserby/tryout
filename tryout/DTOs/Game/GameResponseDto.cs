using tryout.DTOs.Genre;

namespace tryout.DTOs.Game
{
    public class GameResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int ReleaseYear { get; set; }
        public decimal Rating { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime EditedAt { get; set; }        
        public GenreResponseDto Genre { get; set; } = null!;
    }
}
