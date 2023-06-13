using Fiorello.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Fiorello.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _appDbContext;
        

        public ProductController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            var products = _appDbContext.Products
                .Include(p => p.Category)
                .Include(p => p.Images)
                .Take(2)
                .ToList();
            ViewBag.ProductsCount = _appDbContext.Products.Count();
            return View(products);
            
        }
        public IActionResult LoadMore(int skip)
        {
            var products = _appDbContext.Products
            .Include(p => p.Category)
            .Include(p => p.Images)
            .Skip(skip)
            .Take(2)
            .ToList();


            return PartialView("_LoadMorePartialView", products);
        }
    }
}
