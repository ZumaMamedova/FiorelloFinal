namespace Fiorello.Models.Practice
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<BookGenre> Genres { get; set; } = new List<BookGenre>();

    }
}
