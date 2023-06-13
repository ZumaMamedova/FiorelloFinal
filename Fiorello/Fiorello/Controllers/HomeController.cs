using Fiorello.DAL;
using Fiorello.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fiorello.Controllers
{
    public class HomeController : Controller
    {

        private readonly AppDbContext _appDbContext;

        public HomeController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new();
            homeVM.Sliders = _appDbContext.Sliders.AsNoTracking().ToList();
            homeVM.SliderContent=_appDbContext.SliderContents.FirstOrDefault();
            homeVM.Products = _appDbContext.Products.Include(p => p.Images).Take(4).ToList();
            homeVM.Categories=_appDbContext.Categories.ToList();
            
           
            return View(homeVM);
        }
    }
}
