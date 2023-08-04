using System;

namespace RedFalcon.Application.DTOs
{
    public class ViewOrganizationDTO : OrganizationBaseDTO
    {
        public int Id { get; set; }

        // Add Custom View Fields or Formatted Data
    }

    public class CreateOrganizationDTO : OrganizationBaseDTO
    {
        // Add Custom View Fields or Formatted Data
    }

    public class UpdateOrganizationDTO : OrganizationBaseDTO
    {
        public int Id { get; set; }

        // Add Custom View Fields or Formatted Data
    }

    public class OrganizationBaseDTO
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
