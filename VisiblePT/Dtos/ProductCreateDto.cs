using System.ComponentModel.DataAnnotations;

namespace VisiblePT.Dtos
{
    public class ProductCreateDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;
        [Required]
        [StringLength(300)]
        public string Description { get; set; } = null!;
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0.")]
        public decimal Price { get; set; }
        [Required]
        public string ImageURL { get; set; } = null!;
    }
}
