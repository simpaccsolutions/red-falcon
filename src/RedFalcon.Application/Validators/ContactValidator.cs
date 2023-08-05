using RedFalcon.Application.Interfaces.Validator;
using RedFalcon.Domain.Entities;

namespace RedFalcon.Application.Validators
{
    public class ContactValidator : IContactValidator
    {
        public async Task<bool> ValidateData(Contact value)
        {
            if(value == null)
                return false;

            if(!ValidateFirstname(value.Firstname))
                return false;

            if (!ValidateEmail(value.Email))
                return false;

            return true;
        }

        private bool ValidateFirstname(string firstname)
        {
            if (string.IsNullOrEmpty(firstname))
                return false;

            if (firstname.Length > 100)
                return false;

            return true;
        }

        private bool ValidateEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            if (email.Length > 100)
                return false;

            return true;
        }
    }
}
