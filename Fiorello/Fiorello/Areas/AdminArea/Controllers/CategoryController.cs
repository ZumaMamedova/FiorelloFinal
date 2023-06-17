using Fiorello.DAL;
using Fiorello.Models;
using Fiorello.ViewModels.AdminVM.Category;
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
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(CategoryCreateVM category)
        {
            if (!ModelState.IsValid) return View();
            var exist=_appDbContext.Categories.Any(c=>c.Name.ToLower() == category.Name.ToLower());
            if (exist)
            {
                ModelState.AddModelError("Name", "Eyni adli category movcuddur");
                return View();
            }
            Category newCategory = new();
            newCategory.Name = category.Name;
            newCategory.Desc = category.Desc;
            _appDbContext.Categories.Add(newCategory);
            _appDbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int?id)
        {
            if (id == null) return NotFound();
            var category = _appDbContext.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null) return NotFound();
            return View(new CategoryUpdateVM { Name=category.Name,Desc=category.Desc});
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Update(int?id,CategoryUpdateVM categoryUpdateVm) 
        {
            if (!ModelState.IsValid) return View();
            var category = _appDbContext.Categories.FirstOrDefault(c => c.Id == id);
            var exist = _appDbContext.Categories.Any(c => c.Name.ToLower() == categoryUpdateVm.Name.ToLower()&& c.Id!=id);
            if (exist)
            {
                ModelState.AddModelError("Name", "Eyni adli category movcuddur");
                return View();
            }
            category.Name = categoryUpdateVm.Name;
            category.Desc= categoryUpdateVm.Desc;
            _appDbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var category = _appDbContext.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null) return NotFound();
            _appDbContext.Categories.Remove(category);
            _appDbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
