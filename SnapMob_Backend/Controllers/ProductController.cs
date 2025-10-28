using Microsoft.AspNetCore.Authorization;
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

      
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllProducts([FromQuery] ProductQueryDTO query)
        {
            var result = await _productService.GetProductsAsync(query);
            return Ok(new ApiResponse<ProductListResponseDTO>(200, "Products fetched successfully", result));
        }

       
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound(new ApiResponse<string>(404, "Product not found"));

            return Ok(new ApiResponse<ProductDTO>(200, "Product fetched successfully", product));
        }

       
        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> AddProduct([FromForm] ProductCreateUpdateDTO dto)
        {
            var created = await _productService.AddProductAsync(dto);
            return CreatedAtAction(nameof(GetProductById), new { id = created.Id },
                new ApiResponse<ProductDTO>(201, "Product created successfully", created));
        }

       
        [HttpPut("{id}")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> UpdateProduct(int id, [FromForm] ProductCreateUpdateDTO dto)
        {
            var updated = await _productService.UpdateProductAsync(id, dto);
            if (updated == null)
                return NotFound(new ApiResponse<string>(404, "Product not found"));

            return Ok(new ApiResponse<ProductDTO>(200, "Product updated successfully", updated));
        }

       
        [HttpDelete("{id}")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var deleted = await _productService.DeleteProductAsync(id);
            if (!deleted)
                return NotFound(new ApiResponse<string>(404, "Product not found"));

            return Ok(new ApiResponse<string>(200, "Product deleted successfully"));
        }
    }
}
