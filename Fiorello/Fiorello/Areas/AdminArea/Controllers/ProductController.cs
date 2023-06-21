using Fiorello.DAL;
using Fiorello.Helper;
using Fiorello.Models;
using Fiorello.ViewModels.AdminVM.Category;
using Fiorello.ViewModels.AdminVM.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
namespace Fiorello.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(AppDbContext appDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _appDbContext = appDbContext;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            var products = _appDbContext.Products
                .Include(p => p.Images)
                .Include(p => p.Category)
                .ToList();
            return View(products);
        }

        public IActionResult Create()
        {
           
            ViewBag.Categories=new SelectList(_appDbContext.Categories.ToList(),"Id","Name");
            return View();
        }
        [HttpPost]
        public IActionResult Create(ProductCreateVM productCreateVM)
        {
            if(!ModelState.IsValid) return View();
            Product product = new();
            foreach (var item in productCreateVM.Photos)
            {
                if(!item.CheckFileType()) 
                {
                    ModelState.AddModelError("Photos", "choose correct one");
                    return View();
                }
                if (item.CheckFileSize(1000))
                {
                    ModelState.AddModelError("Photos", "choose correct size");
                    return View();
                }

                ProductImage image = new();
                if (item == productCreateVM.Photos[0])
                {
                    image.isMain = true;
                }

                image.ImageUrl = item.SaveImage(_webHostEnvironment, "img");
                product.Images.Add(image);

            }
           
            product.Name = productCreateVM.Name;
            product.Price = productCreateVM.Price;
            product.CategoryId=productCreateVM.CategoryId;
            product.Price=productCreateVM.Price;
            _appDbContext.Products.Add(product);
            _appDbContext.SaveChanges();
            return RedirectToAction("Index");   
        }


        public IActionResult Detail(int? id)
        {
            if (id == null) return NotFound();
            var product = _appDbContext.Products.FirstOrDefault(c => c.Id == id);
            if (product == null) return NotFound();
            return View(product);
        }
       
        public IActionResult Delete(int? id)
            {
                if (id == null) return NotFound();
                var product = _appDbContext.Products.FirstOrDefault(c => c.Id == id);
                if (product == null) return NotFound();
                string path = Path.Combine(_webHostEnvironment.WebRootPath, "img", product.Name);
                HelperDelete.DeleteFile(path);
                _appDbContext.Products.Remove(product);
                _appDbContext.SaveChanges();
                return RedirectToAction("Index");
            }

        public IActionResult Update(int? id)
        {
            if (id == null) return NotFound();
            var product = _appDbContext.Products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            return View(new ProductUpdateVM { Name = product.Name, Price = product.Price});
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Update(int? id, ProductUpdateVM productUpdateVm)
        {
            if (!ModelState.IsValid) return View();
            var product = _appDbContext.Products.FirstOrDefault(c => c.Id == id);
            var exist = _appDbContext.Products.Any(p => p.Name.ToLower() == productUpdateVm.Name.ToLower() && p.Id != id);
            if (exist)
            {
                ModelState.AddModelError("Name", "Eyni adli product movcuddur");
                return View();
            }
            product.Name = productUpdateVm.Name;
            product.Price = productUpdateVm.Price;
            _appDbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

    }
}
