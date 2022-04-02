using Xunit;
using FluentAssertions;

namespace NoteIt.Application.Functions.Contacts.Commands.UpdateContact.Tests
{
    public class UpdateContactCommandValidatorTests
    {
        [Fact]
        public void Validate_ForEmptyNameReturnInvalidValidation()
        {
            // Arrange
            var validator = new UpdateContactCommandValidator();
            var command = new UpdateContactCommand()
            {
                Id = 1,
                Name = "",
                EmailAddress = "Email100@email.com",
                PhoneNumber = "+48987654321",
            };
            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }

        [Fact()]
        public void Validate_ForTooLongNameReturnInvalidValidation()
        {
            // Arrange
            var validator = new UpdateContactCommandValidator();
            var command = new UpdateContactCommand()
            {
                Id = 1,
                Name = new string('A', 257),
                EmailAddress = "Email1@email.com",
                PhoneNumber = "+48987654321",
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }

        [Theory()]
        [InlineData("email.com")]
        [InlineData("ExampleEmail")]
        public void Validate_ForInvalidEmailFormatReturnInvalidValidation(string email)
        {
            // Arrange
            var validator = new UpdateContactCommandValidator();
            var command = new UpdateContactCommand()
            {
                Id = 1,
                Name = "Name1",
                EmailAddress = email,
                PhoneNumber = "+48987654321",
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }

        [Fact()]
        public void Validate_ForTooLongEmailReturnInvalidValidation()
        {
            // Arrange
            var validator = new UpdateContactCommandValidator();
            var command = new UpdateContactCommand()
            {
                Id = 1,
                Name = "Name1",
                EmailAddress = new string('A', 119) + "@email.com",
                PhoneNumber = "+48987654321",
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }

        [Fact()]
        public void Validate_ForTooShortPhoneReturnInvalidValidation()
        {
            // Arrange
            var validator = new UpdateContactCommandValidator();
            var command = new UpdateContactCommand()
            {
                Id = 1,
                Name = "Name1",
                EmailAddress = "Email1@email.com",
                PhoneNumber = new string('A', 8),
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }

        [Fact()]
        public void Validate_ForTooLongPhoneReturnInvalidValidation()
        {
            // Arrange
            var validator = new UpdateContactCommandValidator();
            var command = new UpdateContactCommand()
            {
                Id = 1,
                Name = "Name1",
                EmailAddress = "Email1@email.com",
                PhoneNumber = new string('A', 16),
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }
    }
}