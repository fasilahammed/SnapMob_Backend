using SnapMob_Backend.DTO.AuthDTO;

namespace SnapMob_Backend.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto);
        Task<AuthResponseDto> LoginAsync(LoginDTO loginDto);
    }
}
