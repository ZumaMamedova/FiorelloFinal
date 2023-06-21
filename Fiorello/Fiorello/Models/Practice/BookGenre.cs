namespace Fiorello.Models.Practice
{
    public class BookGenre
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int GenreID { get; set; }
        public Genre Genre { get; set; }

    }
}
