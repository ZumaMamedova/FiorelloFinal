using System.ComponentModel.DataAnnotations.Schema;

namespace Fiorello.Models.Practice
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<BookGenre> BookGenres { get; set; } = new List<BookGenre>();
        //[NotMapped]
        //public List<int> GenreIds { get; set; }
    }
}
