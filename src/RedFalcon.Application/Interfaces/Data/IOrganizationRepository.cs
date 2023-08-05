using RedFalcon.Application.ResourceParameters;
using RedFalcon.Domain.Entities;

namespace RedFalcon.Application.Interfaces.Data
{
    public interface IOrganizationRepository : IBaseRepository<Organization>
    {
        Task<(IEnumerable<Organization> organizations, int recordCount)> GetAsync(OrganizationResourceParameters resourceParameters);
    }
}
