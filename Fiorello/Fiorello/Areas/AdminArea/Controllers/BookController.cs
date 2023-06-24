using Fiorello.DAL;
using Fiorello.Models.Practice;
using Fiorello.ViewModels.AdminVM.Book;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Fiorello.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class BookController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public BookController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            var data = _appDbContext.Books
                .Include(b => b.BookGenres)
                .ThenInclude(bg=>bg.Genre)
                .ToList();
            return View(data);
        }

        public IActionResult Create() 
        {
            ViewBag.Genres = new SelectList(_appDbContext.Genres.ToList(),"Id","Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(BookCreateVM bookCreateVM)
        {
            ViewBag.Genres = new SelectList(_appDbContext.Genres.ToList(), "Id", "Name");
            Book book = new();
            book.Name = bookCreateVM.Name;
            List<BookGenre>bookGenres=new List<BookGenre>();
            foreach(var item in bookCreateVM.GenresIds)
            {
                BookGenre bookGenre = new BookGenre();
                bookGenre.BookId = book.Id;
                bookGenre.GenreID = item;
                bookGenres.Add(bookGenre);
            }
            book.BookGenres=bookGenres;
            _appDbContext.Books.Add(book);
            _appDbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
