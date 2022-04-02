using Xunit;
using System;
using FluentAssertions;

namespace NoteIt.Application.Functions.Storages.Commands.CreateStorage.Tests
{
    public class CreateStorageCommandValidatorTests
    {
        [Fact()]
        public void Validate_ForEmptyNameReturnInvalidValidation()
        {
            // Arrange
            var validator = new CreateStorageCommandValidator();
            var command = new CreateStorageCommand()
            {
                Id = Guid.NewGuid(),
                Name = "",
                Password = "Pass12345"
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }

        [Fact()]
        public void Validate_ForTooShortNameReturnInvalidValidation()
        {
            // Arrange
            var validator = new CreateStorageCommandValidator();
            var command = new CreateStorageCommand()
            {
                Id = Guid.NewGuid(),
                Name = new string('A', 2),
                Password = "Pass12345"
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
            var validator = new CreateStorageCommandValidator();
            var command = new CreateStorageCommand()
            {
                Id = Guid.NewGuid(),
                Name = new string('A', 65),
                Password = "Pass12345"
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }

        [Theory()]
        [InlineData("Aa!")]
        [InlineData("Aa@")]
        [InlineData("Aa#")]
        [InlineData("Aa%")]
        [InlineData("Aa&")]
        public void Validate_ForNonMatchingCharactersInNameReturnInvalidValidation(string name)
        {
            // Arrange
            var validator = new CreateStorageCommandValidator();
            var command = new CreateStorageCommand()
            {
                Id = Guid.NewGuid(),
                Name = name,
                Password = "Pass12345"
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }

        [Fact()]
        public void Validate_ForEmptyPasswordReturnInvalidValidation()
        {
            // Arrange
            var validator = new CreateStorageCommandValidator();
            var command = new CreateStorageCommand()
            {
                Id = Guid.NewGuid(),
                Name = "Name",
                Password = ""
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }

        [Fact()]
        public void Validate_ForTooLongPasswordReturnInvalidValidation()
        {
            // Arrange
            var validator = new CreateStorageCommandValidator();
            var command = new CreateStorageCommand()
            {
                Id = Guid.NewGuid(),
                Name = "Name",
                Password = new string('A', 65),
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }

        [Fact()]
        public void Validate_ForTooShortPasswordReturnInvalidValidation()
        {
            // Arrange
            var validator = new CreateStorageCommandValidator();
            var command = new CreateStorageCommand()
            {
                Id = Guid.NewGuid(),
                Name = "Name",
                Password = new string('A', 2)
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }

        [Theory()]
        [InlineData("Pass 123")]
        [InlineData("Pass123 ")]
        [InlineData(" Pass123")]
        public void Validate_ForWhiteSpaceInPasswordReturnInvalidValidation(string password)
        {
            // Arrange
            var validator = new CreateStorageCommandValidator();
            var command = new CreateStorageCommand()
            {
                Id = Guid.NewGuid(),
                Name = "Name",
                Password = password
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }
    }
}