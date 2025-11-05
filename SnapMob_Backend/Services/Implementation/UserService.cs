using AutoMapper;
using SnapMob_Backend.Common;
using SnapMob_Backend.DTOs.UserDTOs;
using SnapMob_Backend.Models;
using SnapMob_Backend.Repositories.interfaces;
using SnapMob_Backend.Repositories.Interfaces;
using SnapMob_Backend.Services.Interfaces;


namespace SnapMob_Backend.Services.implementation
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _genericRepo;
        private readonly IUserRepository _userRepo;
        private readonly CloudinaryService _cloudinaryService;
        private readonly IMapper _mapper;

        public UserService(IGenericRepository<User> genericRepo, IUserRepository userRepo, IMapper mapper, CloudinaryService cloudinaryService)
        {
            _genericRepo = genericRepo;
            _userRepo = userRepo;
            _mapper = mapper;
            _cloudinaryService=cloudinaryService;
        }

        public async Task<ApiResponse<IEnumerable<UserDTO>>> GetAllUsersAsync()
        {
            var users = await _genericRepo.GetAllAsync();
            var activeUsers = users.Where(u => !u.IsDeleted);

            var mapped = _mapper.Map<IEnumerable<UserDTO>>(activeUsers);
            return new ApiResponse<IEnumerable<UserDTO>>(200, "Users retrieved successfully", mapped);
        }

        public async Task<ApiResponse<UserDTO>> GetUserByIdAsync(int id)
        {
            var user = await _genericRepo.GetByIdAsync(id);
            if (user == null || user.IsDeleted)
                return new ApiResponse<UserDTO>(404, "User not found");

            var mapped = _mapper.Map<UserDTO>(user);
            return new ApiResponse<UserDTO>(200, "User retrieved successfully", mapped);
        }

        

        public async Task<ApiResponse<string>> BlockUnblockUserAsync(int id)
        {
            var user = await _genericRepo.GetByIdAsync(id);
            if (user == null || user.IsDeleted)
                return new ApiResponse<string>(404, "User not found");

            if (user.Role == Roles.admin)
                return new ApiResponse<string>(403, "Action forbidden. Admin users cannot be modified.");

            bool wasBlocked = user.IsBlocked;
            await _userRepo.BlockUnblockUserAsync(id);

            string msg = wasBlocked ? "User unblocked successfully" : "User blocked successfully";
            return new ApiResponse<string>(200, msg);
        }

        public async Task<ApiResponse<string>> SoftDeleteUserAsync(int id)
        {
            var user = await _genericRepo.GetByIdAsync(id);
            if (user == null || user.IsDeleted)
                return new ApiResponse<string>(404, "User not found");

            if (user.Role == Roles.admin)
                return new ApiResponse<string>(403, "Action forbidden. Admin users cannot be deleted.");

            await _userRepo.SoftDeleteUserAsync(id);
            return new ApiResponse<string>(200, "User soft-deleted successfully");
        }

        public async Task<ApiResponse<string>> ChangePasswordAsync(int userId, ChangePasswordDTO dto)
        {
            var user = await _genericRepo.GetByIdAsync(userId);
            if (user == null || user.IsDeleted)
                return new ApiResponse<string>(404, "User not found");

            if (user.Role == Roles.admin)
                return new ApiResponse<string>(403, "Admin password cannot be changed through this route");

            // ✅ Verify old password
            bool isOldPasswordValid = BCrypt.Net.BCrypt.Verify(dto.OldPassword, user.PasswordHash);
            if (!isOldPasswordValid)
                return new ApiResponse<string>(400, "Old password is incorrect");

            // ✅ Hash new password
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
            user.ModifiedOn = DateTime.UtcNow;

            await _genericRepo.UpdateAsync(user);

            return new ApiResponse<string>(200, "Password changed successfully");
        }
        public async Task<ApiResponse<string>> UpdateUserProfileAsync(int id, UserUpdateDTO dto)
        {
            var user = await _genericRepo.GetByIdAsync(id);
            if (user == null || user.IsDeleted)
                return new ApiResponse<string>(404, "User not found");

            if (!string.IsNullOrEmpty(dto.Name))
                user.Name = dto.Name;

            if (!string.IsNullOrEmpty(dto.Email))
                user.Email = dto.Email;

            if (!string.IsNullOrEmpty(dto.PhoneNumber))
                user.PhoneNumber = dto.PhoneNumber;

            if (!string.IsNullOrEmpty(dto.Address))
                user.Address = dto.Address;

            // ✅ Upload new image if provided
            if (dto.ProfileImage != null)
            {
                var uploadResult = await _cloudinaryService.UploadImageAsync(dto.ProfileImage);
                user.ProfileImage = uploadResult.SecureUrl.ToString();
            }

            user.ModifiedOn = DateTime.UtcNow;
            user.ModifiedBy = "user"; // later replace from JWT

            await _genericRepo.UpdateAsync(user);
            return new ApiResponse<string>(200, "Profile updated successfully");
        }


    }
}
