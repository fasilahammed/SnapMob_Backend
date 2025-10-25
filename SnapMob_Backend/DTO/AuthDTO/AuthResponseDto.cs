using System.Text.Json.Serialization;

namespace SnapMob_Backend.DTO.AuthDTO
{
    public class AuthResponseDto
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = null!;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? AccessToken { get; set; }

        public AuthResponseDto(int statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }

        public AuthResponseDto(int statusCode, string message, string accessToken)
        {
            StatusCode = statusCode;
            Message = message;
            AccessToken = accessToken;
        }
    }
}