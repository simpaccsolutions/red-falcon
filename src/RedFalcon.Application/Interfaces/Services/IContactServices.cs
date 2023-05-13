using RedFalcon.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedFalcon.Application.Interfaces.Services
{
    public interface IContactServices
    {
        Task<ViewContactDTO?> GetByIdAsync(int contactId);
        Task<IEnumerable<ViewContactDTO?>> GetAsync();
        Task<ViewContactDTO?> CreateAsync(CreateContactDTO contact);
        Task<bool> UpdateAsync(int contactId, UpdateContactDTO contact);
        Task<bool> DeleteAsync(int contactId);
    }
}
