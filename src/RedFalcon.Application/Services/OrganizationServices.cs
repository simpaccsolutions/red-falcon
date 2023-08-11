using AutoMapper;
using Microsoft.Extensions.Logging;
using RedFalcon.Application.DTOs;
using RedFalcon.Application.Interfaces.Data;
using RedFalcon.Application.Interfaces.Services;
using RedFalcon.Application.Interfaces.Validator;
using RedFalcon.Application.ResourceParameters;
using RedFalcon.Application.ResultModels;
using RedFalcon.Domain.Entities;

namespace RedFalcon.Application.Services
{
    public class OrganizationServices : IOrganizationServices
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IMapper _mapper;
        private readonly ILogger<OrganizationServices> _logger;
        private readonly IOrganizationValidator _validator;

        public OrganizationServices(IUnitOfWork unitofwork, IMapper mapper, ILogger<OrganizationServices> logger, IOrganizationValidator validator)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
            _logger = logger;
            _validator = validator;
        }

        public async Task<ViewOrganizationDTO?> CreateAsync(CreateOrganizationDTO organization)
        {
            try
            {
                var record = _mapper.Map<Organization>(organization);
                record.CreatedBy = "1";
                record.DateCreated = DateTime.UtcNow;

                await _validator.ValidateData(record);

                _unitofwork.CreateTransaction();

                await _unitofwork.Organizations.CreateAsync(record);

                _unitofwork.Commit();

                return _mapper.Map<ViewOrganizationDTO>(record);

            }
            catch (Exception ex)
            {
                _unitofwork.Rollback();
                _logger.LogError($@"{ex.Message}");
            }

            return null;
        }

        public async Task<bool> DeleteAsync(int organizationId)
        {
            try
            {
                var record = await _unitofwork.Organizations.GetByIdAsync(organizationId);
                if (record == null)
                {
                    return false;
                }

                _unitofwork.CreateTransaction();
                await _unitofwork.Organizations.DeleteAsync(record.Id);
                _unitofwork.Commit();

                return true;
            }
            catch (Exception ex)
            {
                _unitofwork.Rollback();
                _logger.LogError($@"{ex.Message}");
            }

            return false;
        }

        public async Task<PaginatedList<ViewOrganizationDTO>> GetAsync(OrganizationResourceParameters resourceParameters)
        {
            resourceParameters.SearchFields = new List<string> { "Name", "Description" };
            var result = await _unitofwork.Organizations.GetAsync(resourceParameters).ConfigureAwait(false);

            var paginatedResult = new PaginatedList<ViewOrganizationDTO>(
                _mapper.Map<IEnumerable<ViewOrganizationDTO>>(result.organizations).ToList(),
                result.recordCount,
                resourceParameters.Page,
                resourceParameters.PageSize);

            return paginatedResult;
        }

        public async Task<ViewOrganizationDTO?> GetByIdAsync(int organizationId)
        {
            var record = await _unitofwork.Organizations.GetByIdAsync(organizationId).ConfigureAwait(false);

            return _mapper.Map<ViewOrganizationDTO>(record);
        }

        public async Task<bool> UpdateAsync(int organizationId, UpdateOrganizationDTO organization)
        {
            try
            {
                var record = await _unitofwork.Organizations.GetByIdAsync(organizationId).ConfigureAwait(false);
                if (record == null)
                    return false;

                _mapper.Map(organization, record);

                _unitofwork.CreateTransaction();
                await _unitofwork.Organizations.UpdateAsync(record).ConfigureAwait(false);
                _unitofwork.Commit();

                return true;
            }
            catch (Exception ex)
            {
                _unitofwork.Rollback();
                _logger.LogError($@"{ex.Message}");
                return false;
            }
        }
    }
}
