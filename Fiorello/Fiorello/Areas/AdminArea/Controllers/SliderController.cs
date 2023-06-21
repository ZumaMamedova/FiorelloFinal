using Fiorello.DAL;
using Fiorello.Helper;
using Fiorello.Models;
using Fiorello.ViewModels.AdminVM.Slider;
using Microsoft.AspNetCore.Mvc;

namespace Fiorello.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SliderController(AppDbContext appDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _appDbContext = appDbContext;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View(_appDbContext.Sliders.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(SliderCreateVM sliderCreateVm)
        {
            if (sliderCreateVm.Photo == null)
            {
                ModelState.AddModelError("Photo", "bos qoyma");
                return View();
            }

            if (!sliderCreateVm.Photo.CheckFileType())
            {
                ModelState.AddModelError("Photo", "choose the correct one");
                return View();
            }

            if (sliderCreateVm.Photo.CheckFileSize(1000))
            {
                ModelState.AddModelError("Photo", "boyuk olcu");
                return View();
            }


            Slider slider = new();
            slider.ImageUrl = sliderCreateVm.Photo.SaveImage(_webHostEnvironment, "img");
            _appDbContext.Sliders.Add(slider);
            _appDbContext.SaveChanges();
            return RedirectToAction("index");


        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var slider = _appDbContext.Sliders.FirstOrDefault(c => c.Id == id);
            if (slider == null) return NotFound();
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "img",slider.ImageUrl);
            HelperDelete.DeleteFile(path);
            _appDbContext.Sliders.Remove(slider);
            _appDbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
