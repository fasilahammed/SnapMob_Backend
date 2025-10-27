using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SnapMob_Backend.Common;
using SnapMob_Backend.DTO.ProductDTO;
using SnapMob_Backend.DTOs;
using SnapMob_Backend.Services.Services.interfaces;

namespace SnapMob_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductBrandController : ControllerBase
    {
        private readonly IProductBrandService _brandService;

        public ProductBrandController(IProductBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var brands = await _brandService.GetAllBrandsAsync();
            return Ok(new ApiResponse<IEnumerable<ProductBrandDTO>>(200, "Brands fetched successfully", brands));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var brand = await _brandService.GetBrandByIdAsync(id);
            if (brand == null)
                return NotFound(new ApiResponse<string>(404, "Brand not found"));
            return Ok(new ApiResponse<ProductBrandDTO>(200, "Brand fetched successfully", brand));
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Create([FromBody] ProductBrandDTO dto)
        {
            var created = await _brandService.CreateBrandAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                new ApiResponse<ProductBrandDTO>(201, "Brand created successfully", created));
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductBrandDTO dto)
        {
            var updated = await _brandService.UpdateBrandAsync(id, dto);
            if (updated == null)
                return NotFound(new ApiResponse<string>(404, "Brand not found"));
            return Ok(new ApiResponse<ProductBrandDTO>(200, "Brand updated successfully", updated));
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _brandService.DeleteBrandAsync(id);
            if (!deleted)
                return NotFound(new ApiResponse<string>(404, "Brand not found"));
            return Ok(new ApiResponse<string>(200, "Brand deleted successfully"));
        }
    }
}
