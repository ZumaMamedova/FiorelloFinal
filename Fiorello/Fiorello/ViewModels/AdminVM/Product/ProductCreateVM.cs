using Fiorello.Models;
using System.ComponentModel.DataAnnotations;

namespace Fiorello.ViewModels.AdminVM.Product
{
    public class ProductCreateVM
    {
        [Required]
        [StringLength(100)]
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public int StockCount { get; set; }
        [Required]
        public IFormFile[] Photos { get; set; }
    }
}
