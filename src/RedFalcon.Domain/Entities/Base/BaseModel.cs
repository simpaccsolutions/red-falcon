using System;
using System.Collections.Generic;
using System.Text;

namespace RedFalcon.Domain.Entities.Base
{
    public class BaseModel
    {
        // Key
        public int Id { get; set; }

        // Row attribute
        public bool IsDeleted { get; set; }

        // Audit Fields
        public string CreatedBy { get; set; } = null!;
        public string? UpdatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
}
