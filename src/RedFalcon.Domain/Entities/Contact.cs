using RedFalcon.Domain.Entities.Base;

namespace RedFalcon.Domain.Entities
{
    public class Contact: BaseModel
    {
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public DateTime? BirthDate { get; set; }
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;

        public int? OrganizationId { get; set; }

        public Organization Organization { get; set; } = null!;
    }
}
