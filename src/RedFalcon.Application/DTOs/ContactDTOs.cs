using System;

namespace RedFalcon.Application.DTOs
{
    public class ViewContactDTO : ContactBaseDTO
    {
        public int Id { get; set; }

        // Add Custom View Fields or Formatted Data
        public string FullName { get { return $@"{Firstname} {Lastname}"; } }
    }

    public class CreateContactDTO : ContactBaseDTO
    {
        // Add Custom View Fields or Formatted Data
    }

    public class UpdateContactDTO : ContactBaseDTO
    {
        // Add Custom View Fields or Formatted Data
    }

    public class ContactBaseDTO
    {
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public DateTime? BirthDate { get; set; }
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
    }
}
