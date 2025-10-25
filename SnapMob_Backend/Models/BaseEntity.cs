namespace SnapMob_Backend.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; } = "system";
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; } = "system";
        public DateTime? DeletedOn { get; set; }
        public string DeletedBy { get; set; } = "system";
        public bool IsDeleted { get; set; } = false;
    }
}
