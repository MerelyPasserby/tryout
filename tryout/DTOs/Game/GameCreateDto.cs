using tryout.Models;
namespace tryout.DTOs.Game
{
    public class GameCreateDto
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int ReleaseYear { get; set; }      
        public int GenreId { get; set; }        
    }
}
