
using SnapMob_Backend.DTO.AuthDTO;

namespace SnapMob_Backend.Services.Services.interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto);
        Task<AuthResponseDto> LoginAsync(LoginDTO loginDto);

        //Task<bool> RevokeTokenAsync(string token, string ipAddress);
        //Task<AuthResponseDto> RefreshTokenAsync(string token, string ipAddress);
    }
}
