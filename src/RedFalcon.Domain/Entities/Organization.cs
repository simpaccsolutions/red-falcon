using RedFalcon.Domain.Entities.Base;

namespace RedFalcon.Domain.Entities
{
    public class Organization: BaseModel
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        
        public IEnumerable<Contact> Contacts { get; set; } = null!;
    }
}
