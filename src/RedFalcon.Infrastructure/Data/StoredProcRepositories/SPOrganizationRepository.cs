using Dapper;
using RedFalcon.Application.Interfaces.Data;
using RedFalcon.Application.ResourceParameters;
using RedFalcon.Domain.Entities;
using System.Data;

namespace RedFalcon.Infrastructure.Data.StoredProcRepositories
{
    public class SPOrganizationRepository : IOrganizationRepository
    {
        private readonly DatabaseSession _dbSession;

        public SPOrganizationRepository(DatabaseSession dbSession)
        {
            _dbSession = dbSession;
        }

        public async Task<Organization> CreateAsync(Organization organization)
        {
            var query = $@"sp_CreateOrganization";

            var queryParams = new
            {
                Name = organization.Name,
                Description = organization.Description,
                CreatedBy = organization.CreatedBy,
                DateCreated = organization.DateCreated,
            };

            organization.Id = await _dbSession.Connection.ExecuteScalarAsync<int>(query, queryParams, _dbSession.Transaction, commandType: CommandType.StoredProcedure);

            return organization;
        }

        public async Task<bool> DeleteAsync(int organizationId)
        {
            var query = $@"sp_DeleteOrganization";

            var queryParams = new
            {
                OrganizationId = organizationId
            };

            await _dbSession.Connection.ExecuteAsync(query, queryParams, _dbSession.Transaction, commandType: CommandType.StoredProcedure);

            return true;
        }

        public async Task<(IEnumerable<Organization> organizations, int recordCount)> GetAsync(OrganizationResourceParameters resourceParameters)
        {
            IEnumerable<Organization> result;

            var dataQuery = $@"SELECT * FROM Organization WHERE IsDeleted=0 ";
            var dataCountQuery = $@"SELECT COUNT(*) FROM Organization WHERE IsDeleted=0 ";

            var queryParamBuilder = new QueryParameters(resourceParameters.Search, resourceParameters.SearchFields, resourceParameters.Page, resourceParameters.PageSize);

            var searchSQLQuery = queryParamBuilder.GetSearchSQLQuery();
            dataQuery += searchSQLQuery;
            dataCountQuery += searchSQLQuery;

            var filterSQLQuery = queryParamBuilder.GetFilterSQLQuery();
            dataQuery += filterSQLQuery;
            dataCountQuery += filterSQLQuery;

            dataQuery += queryParamBuilder.GetPaginationSQLQuery();

            result = await _dbSession.Connection.QueryAsync<Organization>(dataQuery, queryParamBuilder.Parameters);
            var totalCount = await _dbSession.Connection.ExecuteScalarAsync<int>(dataCountQuery, queryParamBuilder.Parameters);

            return (result, totalCount);
        }

        public Task<IEnumerable<Organization>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Organization?> GetByIdAsync(int organizationid)
        {
            var query = $@"sp_GetOrganizationById";
            var queryParams = new
            {
                OrganizationId = organizationid
            };

            var result = await _dbSession.Connection.QueryFirstOrDefaultAsync<Organization>(query, queryParams, commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<bool> UpdateAsync(Organization organization)
        {
            var query = $@"sp_UpdateOrganization";

            var queryParams = new
            {
                Name = organization.Name,
                Description = organization.Description,
                OrganizationId = organization.Id
            };

            await _dbSession.Connection.ExecuteAsync(query, queryParams, _dbSession.Transaction, commandType: CommandType.StoredProcedure);

            return true;
        }
    }
}
