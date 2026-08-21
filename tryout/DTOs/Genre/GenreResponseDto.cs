namespace tryout.DTOs.Genre
{
    public class GenreResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }       
    }
}
