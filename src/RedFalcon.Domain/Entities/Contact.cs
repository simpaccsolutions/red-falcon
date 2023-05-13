using RedFalcon.Domain.Entities.Base;
using System;

namespace RedFalcon.Domain.Entities
{
    public class Contact: BaseModel
    {
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public DateTime? BirthDate { get; set; }
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
    }
}
