using RedFalcon.Application.Interfaces.Validator;
using RedFalcon.Domain.Entities;

namespace RedFalcon.Application.Validators
{
    public class OrganizationValidator : IOrganizationValidator
    {
        public async Task<bool> ValidateData(Organization value)
        {



            return true;
        }
    }
}
