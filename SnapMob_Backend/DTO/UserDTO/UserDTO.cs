namespace SnapMob_Backend.DTOs.UserDTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public bool IsBlocked { get; set; }
        public string CreatedOn { get; set; }

        public string? ProfileImage { get; set; }
    }
}
