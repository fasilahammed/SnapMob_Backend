using Microsoft.AspNetCore.Http;

namespace SnapMob_Backend.DTO.ProductDTO
{
    public class ProductCreateUpdateDTO
    {
        // 🏷️ Basic info
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int CurrentStock { get; set; }

        // 📱 Product specifications
        public string Battery { get; set; } = string.Empty;
        public string Camera { get; set; } = string.Empty;
        public string Storage { get; set; } = string.Empty;
        public string Display { get; set; } = string.Empty;

        public int BrandId { get; set; }
   

        public List<IFormFile> Images { get; set; } = new List<IFormFile>();
    }
}
