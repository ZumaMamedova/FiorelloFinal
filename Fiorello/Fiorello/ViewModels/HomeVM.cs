using Fiorello.Models;

namespace Fiorello.ViewModels
{
    public class HomeVM
    {
        public List<Slider> Sliders { get; set; } = new List<Slider>();
        public SliderContent SliderContent { get; set; }
        public List<Category>Categories { get; set; }
        public List<Product> Products { get; set; }

    }
}
