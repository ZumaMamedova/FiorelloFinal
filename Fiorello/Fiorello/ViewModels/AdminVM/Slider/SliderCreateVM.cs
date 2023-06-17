using System.ComponentModel.DataAnnotations;

namespace Fiorello.ViewModels.AdminVM.Slider
{
    public class SliderCreateVM
    {
        [Required(ErrorMessage ="bos qoyma")]
        public IFormFile Photo { get; set; }
    }
}
