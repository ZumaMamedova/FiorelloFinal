using System.ComponentModel.DataAnnotations;

namespace Fiorello.ViewModels.AdminVM.Product
{
    public class ProductUpdateVM
    {
        [Required]
        [MaxLength(15)]
        public string Name { get; set;}
        [Required]
        public decimal Price { get; set;}
        [Required]
        public string CategoryName { get; set;}

        public string ProductImage { get; set;}
    }
}
