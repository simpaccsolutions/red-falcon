using RedFalcon.Application.ResourceParameters;
using RedFalcon.Domain.Entities;

namespace RedFalcon.Application.Interfaces.Data
{
    public interface IContactRepository : IBaseRepository<Contact>
    {
        Task<(IEnumerable<Contact> contacts, int recordCount)> GetAsync(ContactResourceParameters resourceParameters);
    }
}
