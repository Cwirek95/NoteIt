using Xunit;
using System;
using FluentAssertions;

namespace NoteIt.Application.Functions.Notes.Commands.CreateNote.Tests
{
    public class CreateNoteCommandValidatorTests
    {
        [Fact]
        public void Validate_ForEmptyNameReturnInvalidValidation()
        {
            // Arrange
            var validator = new CreateNoteCommandValidator();
            var command = new CreateNoteCommand()
            {
                Id = 100,
                Name = "",
                Content = "Content100",
                IsImportant = false,
                StorageAddress = "storage1"
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
            var validator = new CreateNoteCommandValidator();
            var command = new CreateNoteCommand()
            {
                Id = 100,
                Name = new string('A', 257),
                Content = "Content100",
                IsImportant = false,
                StorageAddress = "storage1"
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Validate_ForTooLongContentReturnInvalidValidation()
        {
            // Arrange
            var validator = new CreateNoteCommandValidator();
            var command = new CreateNoteCommand()
            {
                Id = 100,
                Name = new string('A', 8001),
                Content = "Content100",
                IsImportant = false,
                StorageAddress = "storage1"
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Validate_ForEmptyStorageAddressReturnInvalidValidation()
        {
            // Arrange
            var validator = new CreateNoteCommandValidator();
            var command = new CreateNoteCommand()
            {
                Id = 100,
                Name = "Name100",
                Content = "Content100",
                IsImportant = false,
                StorageAddress = ""
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }
    }
}