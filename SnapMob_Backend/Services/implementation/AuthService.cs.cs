using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SnapMob_Backend.Data;
using SnapMob_Backend.DTO.AuthDTO;
using SnapMob_Backend.Models;
using SnapMob_Backend.Repositories.interfaces;
using SnapMob_Backend.Services.Services.interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SnapMob_Backend.Services.implementation
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IGenericRepository<User> _userrepo;
        private readonly IConfiguration _configuration;

        public AuthService(AppDbContext appDbContext, IGenericRepository<User> userrepo, IConfiguration configuration)
        {
            _appDbContext = appDbContext;
            _userrepo = userrepo;
            _configuration = configuration;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
        {
            try
            {
                if (registerDto == null)
                {
                    throw new ArgumentNullException(nameof(registerDto), "Register request cannot be null");
                }

                registerDto.Email = registerDto.Email.Trim().ToLower();
                registerDto.Name = registerDto.Name.Trim();
                registerDto.Password = registerDto.Password.Trim();

                var userExist = await _appDbContext.Users
                    .SingleOrDefaultAsync(u => u.Email == registerDto.Email);

                if (userExist != null)
                {
                    return new AuthResponseDto(409, "Email already exists");
                }

                var newUser = new User
                {
                    Email = registerDto.Email,
                    Name = registerDto.Name,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
                    Role = Roles.User 
                };

                await _userrepo.AddAsync(newUser);
                return new AuthResponseDto(200, "Registration successful");
            }
            catch (Exception ex)
            {
                return new AuthResponseDto(500, $"Error adding user: {ex.Message}");
            }
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDTO loginDTO)
        {
            try
            {
                if (loginDTO == null)
                {
                    throw new ArgumentNullException(nameof(loginDTO), "Login request cannot be null");
                }

                loginDTO.Email = loginDTO.Email.Trim().ToLower();
                loginDTO.Password = loginDTO.Password.Trim();

                var user = await _appDbContext.Users
                    .SingleOrDefaultAsync(u => u.Email == loginDTO.Email);

                if (user == null || !BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.PasswordHash))
                {
                    return new AuthResponseDto(401, "Invalid email or password");
                }

                if (user.IsBlocked)
                {
                    return new AuthResponseDto(403, "This account has been blocked");
                }

                var jwtToken = GenerateJwtToken(user);
                return new AuthResponseDto(200, "Login successful", jwtToken);
            }
            catch (Exception ex)
            {
                return new AuthResponseDto(500, $"Error during login: {ex.Message}");
            }
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"] ?? throw new InvalidOperationException("JWT Secret is not configured"));

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString().ToLower())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}