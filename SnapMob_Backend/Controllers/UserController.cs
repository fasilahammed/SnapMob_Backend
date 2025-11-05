using Microsoft.AspNetCore.Mvc;
using SnapMob_Backend.DTOs.UserDTOs;
using SnapMob_Backend.Services.Interfaces;
using System.Threading.Tasks;

namespace SnapMob_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _userService.GetAllUsersAsync();
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var result = await _userService.GetUserByIdAsync(id);
            return StatusCode(result.StatusCode, result);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromForm] UserUpdateDTO dto)
        {
            var response = await _userService.UpdateUserProfileAsync(id, dto);
            return StatusCode(response.StatusCode, response);
        }


        [HttpPatch("block-unblock/{id}")]
        public async Task<IActionResult> BlockUnblockUser(int id)
        {
            var result = await _userService.BlockUnblockUserAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDeleteUser(int id)
        {
            var result = await _userService.SoftDeleteUserAsync(id);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPatch("{id}/change-password")]
        public async Task<IActionResult> ChangePassword(int id, [FromBody] ChangePasswordDTO dto)
        {
            var result = await _userService.ChangePasswordAsync(id, dto);
            return StatusCode(result.StatusCode, result);
        }


    }
}
