using Dapper;
using RedFalcon.Application.Interfaces.Data;
using RedFalcon.Application.ResourceParameters;
using RedFalcon.Domain.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RedFalcon.Infrastructure.Data.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly DatabaseSession _dbSession;

        public ContactRepository(DatabaseSession dbSession)
        {
            _dbSession = dbSession;
        }

        public async Task<Contact> CreateAsync(Contact contact)
        {
            var query = $@"INSERT INTO Contact (Firstname, Lastname, BirthDate, Email, Phone, OrganizationId, IsDeleted, CreatedBy, DateCreated)                            
                            VALUES (@Firstname, @Lastname, @BirthDate, @Email, @Phone, @OrganizationId, 0, @CreatedBy, @DateCreated);
                            SELECT LAST_INSERT_ID();
                            ";

            var queryParams = new
            {
                Firstname = contact.Firstname,
                Lastname = contact.Lastname,
                BirthDate = contact.BirthDate,
                Email = contact.Email,
                Phone = contact.Phone,
                OrganizationId = contact.OrganizationId,
                CreatedBy = contact.CreatedBy,
                DateCreated = contact.DateCreated,
            };

            contact.Id = await _dbSession.Connection.ExecuteScalarAsync<int>(query, queryParams, _dbSession.Transaction);

            return contact;
        }

        public async Task<bool> DeleteAsync(int contactid)
        {
            var query = $@"UPDATE Contact SET IsDeleted=1 WHERE ID=@ID;";

            var queryParams = new
            {
                ID = contactid
            };

            await _dbSession.Connection.ExecuteAsync(query, queryParams, _dbSession.Transaction);

            return true;
        }

        public async Task<(IEnumerable<Contact> contacts, int recordCount)> GetAsync(ContactResourceParameters resourceParameters)
        {
            IEnumerable<Contact> result;

            var dataQuery = $@"SELECT * FROM Contact WHERE IsDeleted=0 ";
            var dataCountQuery = $@"SELECT COUNT(*) FROM Contact WHERE IsDeleted=0 ";

            var queryParamBuilder = new QueryParameters(resourceParameters.Search, resourceParameters.SearchFields, resourceParameters.Page, resourceParameters.PageSize);

            var searchSQLQuery = queryParamBuilder.GetSearchSQLQuery();
            dataQuery += searchSQLQuery;
            dataCountQuery += searchSQLQuery;

            var filterSQLQuery = queryParamBuilder.GetFilterSQLQuery();
            dataQuery += filterSQLQuery;
            dataCountQuery += filterSQLQuery;

            dataQuery += queryParamBuilder.GetPaginationSQLQuery();

            result = await _dbSession.Connection.QueryAsync<Contact>(dataQuery, queryParamBuilder.Parameters);
            var totalCount = await _dbSession.Connection.ExecuteScalarAsync<int>(dataCountQuery, queryParamBuilder.Parameters);

            return (result, totalCount);
        }

        public async Task<Contact?> GetByIdAsync(int contactid)
        {
            var query = $@"SELECT * FROM Contact WHERE IsDeleted=0 AND ID=@ID;";
            var queryParams = new
            {
                ID = contactid
            };

            var result = await _dbSession.Connection.QueryFirstOrDefaultAsync<Contact>(query, queryParams);

            return result;
        }

        public async Task<bool> UpdateAsync(Contact contact)
        {
            var query = $@"UPDATE Contact
                            SET Firstname = @Firstname,
                                Lastname = @Lastname,
                                BirthDate = @BirthDate,
                                Email = @Email,
                                Phone = @Phone,
                                OrganizationId = @OrganizationId
                            WHERE IsDeleted=0 AND ID=@ID;";

            var queryParams = new
            {
                Firstname = contact.Firstname,
                Lastname = contact.Lastname,
                BirthDate = contact.BirthDate,
                Email = contact.Email,
                Phone = contact.Phone,
                OrganizationId = contact.OrganizationId,
                ID = contact.Id
            };

            await _dbSession.Connection.ExecuteAsync(query, queryParams, _dbSession.Transaction);

            return true;
        }
    }
}
