using Xunit;
using System;
using FluentAssertions;

namespace NoteIt.Application.Functions.Contacts.Commands.CreateContact.Tests
{
    public class CreateContactCommandValidatorTests
    {
        [Fact]
        public void Validate_ForEmptyNameReturnInvalidValidation()
        {
            // Arrange
            var validator = new CreateContactCommandValidator();
            var command = new CreateContactCommand()
            {
                Id = 100,
                Name = "",
                EmailAddress = "Email100@email.com",
                PhoneNumber = "+48987654321",
                StorageId = Guid.NewGuid()
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
            var validator = new CreateContactCommandValidator();
            var command = new CreateContactCommand()
            {
                Id = 100,
                Name = new string('A', 257),
                EmailAddress = "Email100@email.com",
                PhoneNumber = "+48987654321",
                StorageId = Guid.NewGuid()
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
            var validator = new CreateContactCommandValidator();
            var command = new CreateContactCommand()
            {
                Id = 100,
                Name = "Name100",
                EmailAddress = email,
                PhoneNumber = "+48987654321",
                StorageId = Guid.NewGuid()
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
            var validator = new CreateContactCommandValidator();
            var command = new CreateContactCommand()
            {
                Id = 100,
                Name = "Name100",
                EmailAddress = new string('A', 119) + "@email.com",
                PhoneNumber = "+48987654321",
                StorageId = Guid.NewGuid()
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
            var validator = new CreateContactCommandValidator();
            var command = new CreateContactCommand()
            {
                Id = 100,
                Name = "Name100",
                EmailAddress = "Email100@email.com",
                PhoneNumber = new string('A', 8),
                StorageId = Guid.NewGuid()
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
            var validator = new CreateContactCommandValidator();
            var command = new CreateContactCommand()
            {
                Id = 100,
                Name = "Name100",
                EmailAddress = "Email100@email.com",
                PhoneNumber = new string('A', 16),
                StorageId = Guid.NewGuid()
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }

        [Fact()]
        public void Validate_ForEmptyStorageIdReturnInvalidValidation()
        {
            // Arrange
            var validator = new CreateContactCommandValidator();
            var command = new CreateContactCommand()
            {
                Id = 100,
                Name = "Name100",
                EmailAddress = "Email100@email.com",
                PhoneNumber = "+48123456789",
                StorageId = Guid.Empty
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }
    }
}