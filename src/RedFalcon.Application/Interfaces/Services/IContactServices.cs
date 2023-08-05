using RedFalcon.Application.DTOs;
using RedFalcon.Application.ResourceParameters;
using RedFalcon.Application.ResultModels;

namespace RedFalcon.Application.Interfaces.Services
{
    public interface IContactServices
    {
        Task<ViewContactDTO?> GetByIdAsync(int contactId);
        Task<PaginatedList<ViewContactDTO>> GetAsync(ContactResourceParameters resourceParameters);
        Task<ViewContactDTO?> CreateAsync(CreateContactDTO contact);
        Task<bool> UpdateAsync(int contactId, UpdateContactDTO contact);
        Task<bool> DeleteAsync(int contactId);
    }
}
