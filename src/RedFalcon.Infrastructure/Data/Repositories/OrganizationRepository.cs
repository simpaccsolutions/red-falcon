﻿using Dapper;
using RedFalcon.Application.Interfaces.Data;
using RedFalcon.Application.ResourceParameters;
using RedFalcon.Domain.Entities;

namespace RedFalcon.Infrastructure.Data.Repositories
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly DatabaseSession _dbSession;

        public OrganizationRepository(DatabaseSession dbSession)
        {
            _dbSession = dbSession;
        }

        public async Task<Organization> CreateAsync(Organization organization)
        {
            var query = $@"INSERT INTO Organization (Name, Description, IsDeleted, CreatedBy, DateCreated)                            
                            VALUES (@Name, @Description, 0, @CreatedBy, @DateCreated);
                            SELECT LAST_INSERT_ID();
                            ";

            var queryParams = new
            {
                Name = organization.Name,
                Description = organization.Description,
                CreatedBy = organization.CreatedBy,
                DateCreated = organization.DateCreated,
            };

            organization.Id = await _dbSession.Connection.ExecuteScalarAsync<int>(query, queryParams, _dbSession.Transaction);

            return organization;
        }

        public async Task<bool> DeleteAsync(int organizationid)
        {
            var query = $@"UPDATE Organization SET IsDeleted=1 WHERE ID=@ID;";

            var queryParams = new
            {
                ID = organizationid
            };

            await _dbSession.Connection.ExecuteAsync(query, queryParams, _dbSession.Transaction);

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
            var query = $@"SELECT * FROM Organization WHERE IsDeleted=0 AND ID=@ID;";
            var queryParams = new
            {
                ID = organizationid
            };

            var result = await _dbSession.Connection.QueryFirstOrDefaultAsync<Organization>(query, queryParams);

            return result;
        }

        public async Task<bool> UpdateAsync(Organization organization)
        {
            var query = $@"UPDATE Organization
                            SET Name = @Name,
                                Description = @Description
                            WHERE IsDeleted=0 AND ID=@ID;";

            var queryParams = new
            {
                Name = organization.Name,
                Description = organization.Description,
                ID = organization.Id
            };

            await _dbSession.Connection.ExecuteAsync(query, queryParams, _dbSession.Transaction);

            return true;
        }
    }
}
