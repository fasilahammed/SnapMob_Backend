using System.Data;

namespace SnapMob_Backend.Models
{
    public class User : BaseEntity
    {
        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;

        public Roles Role { get; set; } = Roles.user;
        public bool IsBlocked { get; set; } = false;
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? ProfileImage { get; set; }
    }
    public enum Roles
    {
        user,
        admin,
        User
    }
}
