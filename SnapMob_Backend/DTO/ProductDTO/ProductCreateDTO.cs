using System.ComponentModel.DataAnnotations;


namespace SnapMob_Backend.DTO.ProductDTO

{
    public class ProductCreateDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int CurrentStock { get; set; }

        [Required]
        public string Battery { get; set; } = string.Empty;

        [Required]
        public string Camera { get; set; } = string.Empty;

        [Required]
        public string Storage { get; set; } = string.Empty;

        [Required]
        public string Display { get; set; } = string.Empty;

        [Required]
        public int BrandId { get; set; }
        

        public List<IFormFile>? Images { get; set; }
    }
}
