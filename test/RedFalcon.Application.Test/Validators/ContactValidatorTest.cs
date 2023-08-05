using RedFalcon.Application.Interfaces.Validator;
using RedFalcon.Application.Validators;
using Xunit;

namespace RedFalcon.Application.Test.Validators
{
    public class ContactValidatorTest
    {
        private readonly IContactValidator _validator;
        public ContactValidatorTest()
        {
            _validator = new ContactValidator();
        }

        [Fact]
        public void Validate_Test_Valid()
        {
            // Arrange
            var contact = new Domain.Entities.Contact
            {
                Firstname = "John",
                Lastname = "Doe",
                Email = "john@app.co",
                Phone = "1234567890",
                BirthDate = DateTime.Parse("1990-01-01"),
            };

            // Act
            var result = _validator.ValidateData(contact);

            // Assert
            Assert.True(result.Result);
        }

        [Fact]
        public void Validate_Test_Invalid_Firstname_Empty()
        {
            // Arrange
            var contact = new Domain.Entities.Contact
            {
                Firstname = "",
                Lastname = "Doe",
                Email = "john@app.co",
                Phone = "1234567890",
                BirthDate = DateTime.Parse("1990-01-01"),
            };

            // Act
            var result = _validator.ValidateData(contact);

            // Assert
            Assert.False(result.Result);
        }

        [Fact]
        public void Validate_Test_Invalid_Firstname_Length()
        {
            // Arrange
            var contact = new Domain.Entities.Contact
            {
                Firstname = "asdfghjkloasdfghjkloasdfghjkloasdfghjkloasdfghjkloasdfghjkloasdfghjkloasdfghjkloasdfghjkloasdfghjkloasdfghjklo",
                Lastname = "Doe",
                Email = "john@app.co",
                Phone = "1234567890",
                BirthDate = DateTime.Parse("1990-01-01"),
            };

            // Act
            var result = _validator.ValidateData(contact);

            // Assert
            Assert.False(result.Result);
        }

        [Fact]
        public void Validate_Test_Invalid_Email_Length()
        {
            // Arrange
            var contact = new Domain.Entities.Contact
            {
                Firstname = "John",
                Lastname = "Doe",
                Email = "asdfghjkloasdfghjkloasdfghjkloasdfghjkloasdfghjkloasdfghjkloasdfghjkloasdfghjkloasdfghjkloasdfghjkloasdfghjklo@app.co",
                Phone = "1234567890",
                BirthDate = DateTime.Parse("1990-01-01"),
            };

            // Act
            var result = _validator.ValidateData(contact);

            // Assert
            Assert.False(result.Result);
        }

    }
}
