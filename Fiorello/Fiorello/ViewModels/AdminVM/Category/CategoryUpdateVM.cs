using System.ComponentModel.DataAnnotations;

namespace Fiorello.ViewModels.AdminVM.Category
{
    public class CategoryUpdateVM
    {
        [Required]
        [MaxLength(10)]
        public string Name { get; set; }
        [Required(ErrorMessage = "bos qoyma")]
        [MinLength(50, ErrorMessage = "It cannot be less than 50")]
        public string Desc { get; set; }
    }
}
