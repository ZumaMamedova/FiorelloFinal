namespace Fiorello.Models.Practice
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<BookGenre> Genres { get; set; } = new List<BookGenre>();
    }
}
