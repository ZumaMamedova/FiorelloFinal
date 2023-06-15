using Fiorello.DAL;
using Fiorello.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fiorello.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public CategoryController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        
        public IActionResult Index()
        {
            return View(_appDbContext.Categories.ToList());
        }

        public IActionResult Detail(int?id)
        {
            if(id == null) return NotFound();
            var category = _appDbContext.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null) return NotFound();
            return View(category);
        }

        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            return Content(category.Name+" "+category.Desc);
        }
    }
}
