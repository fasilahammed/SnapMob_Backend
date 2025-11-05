using SnapMob_Backend.Common;
using SnapMob_Backend.DTOs.UserDTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SnapMob_Backend.Services.Interfaces
{
    public interface IUserService
    {
        Task<ApiResponse<IEnumerable<UserDTO>>> GetAllUsersAsync();
        Task<ApiResponse<UserDTO>> GetUserByIdAsync(int id);
        Task<ApiResponse<string>> UpdateUserProfileAsync(int id, UserUpdateDTO dto);
        Task<ApiResponse<string>> BlockUnblockUserAsync(int id);
        Task<ApiResponse<string>> SoftDeleteUserAsync(int id);
        Task<ApiResponse<string>> ChangePasswordAsync(int userId, ChangePasswordDTO dto);

    }
}
