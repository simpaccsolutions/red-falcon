using RedFalcon.Application.DTOs;
using RedFalcon.Application.ResourceParameters;
using RedFalcon.Application.ResultModels;

namespace RedFalcon.Application.Interfaces.Services
{
    public interface IOrganizationServices
    {
        Task<ViewOrganizationDTO?> GetByIdAsync(int organizationId);
        Task<PaginatedList<ViewOrganizationDTO>> GetAsync(OrganizationResourceParameters resourceParameters);
        Task<ViewOrganizationDTO?> CreateAsync(CreateOrganizationDTO organization);
        Task<bool> UpdateAsync(int organizationId, UpdateOrganizationDTO organization);
        Task<bool> DeleteAsync(int organizationId);
    }
}
