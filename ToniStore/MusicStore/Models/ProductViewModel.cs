using System.ComponentModel.DataAnnotations;

namespace MusicStore.Models
{
    public class ProductViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required(ErrorMessage = "Brand is required")]
        public string BrandName { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public string CategoryName { get; set; }

        [Required]
        [Display(Name = "Supplier")]
        public int SupplierId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; }

        [Required]
        public string Description { get; set; }

        [Url]
        [Required]
        public string ImageUrl { get; set; }
    }
}