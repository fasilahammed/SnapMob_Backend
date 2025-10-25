using Microsoft.AspNetCore.Mvc;
using SnapMob_Backend.Common;
using SnapMob_Backend.DTO.ProductDTO;
using SnapMob_Backend.Services.Services.interfaces;

namespace SnapMob_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // ✅ GET: api/products
        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] ProductQueryDTO query)
        {
            var result = await _productService.GetProductsAsync(query);
            return Ok(new ApiResponse<ProductListResponseDTO>(200, "Products fetched successfully", result));
        }

        // ✅ GET: api/products/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound(new ApiResponse<string>(404, "Product not found"));

            return Ok(new ApiResponse<ProductDTO>(200, "Product fetched successfully", product));
        }
    }
}
