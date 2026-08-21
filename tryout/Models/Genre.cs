namespace tryout.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public ICollection<Game> Games { get; set; } = new List<Game>();
    }
}
