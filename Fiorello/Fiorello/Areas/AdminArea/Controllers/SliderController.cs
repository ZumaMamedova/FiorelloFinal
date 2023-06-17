using Fiorello.DAL;
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

            if (!sliderCreateVm.Photo.ContentType.Contains("Image"))
            {
                ModelState.AddModelError("Photo", "choose the correct one");
                return View();
            }

            if (sliderCreateVm.Photo.Length<1000)
            {
                ModelState.AddModelError("Photo", "boyuk olcu");
                return View();
            }

            string fileName = Guid.NewGuid() + sliderCreateVm.Photo.FileName;
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "img",fileName);
            

            using(FileStream stream=new FileStream(path,FileMode.Create))
            {
                sliderCreateVm.Photo.CopyTo(stream);
            }

            Slider slider = new();
            slider.ImageUrl = fileName;
            _appDbContext.Sliders.Add(slider);
            _appDbContext.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
