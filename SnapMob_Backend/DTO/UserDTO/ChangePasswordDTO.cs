namespace SnapMob_Backend.DTOs.UserDTOs
{
    public class ChangePasswordDTO
    {
        public string OldPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
}
