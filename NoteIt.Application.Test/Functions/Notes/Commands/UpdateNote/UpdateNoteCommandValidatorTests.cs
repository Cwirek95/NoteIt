using Xunit;
using FluentAssertions;

namespace NoteIt.Application.Functions.Notes.Commands.UpdateNote.Tests
{
    public class UpdateNoteCommandValidatorTests
    {
        [Fact]
        public void Validate_ForEmptyNameReturnInvalidValidation()
        {
            // Arrange
            var validator = new UpdateNoteCommandValidator();
            var command = new UpdateNoteCommand()
            {
                Id = 1,
                Name = "",
                Content = "Content1",
                IsImportant = false,
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
            var validator = new UpdateNoteCommandValidator();
            var command = new UpdateNoteCommand()
            {
                Id = 1,
                Name = new string('A', 257),
                Content = "Content1",
                IsImportant = false,
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }

        [Fact()]
        public void Validate_ForTooLongContentReturnInvalidValidation()
        {
            // Arrange
            var validator = new UpdateNoteCommandValidator();
            var command = new UpdateNoteCommand()
            {
                Id = 1,
                Name = "Name1",
                Content = new string('A', 8001),
                IsImportant = false,
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }
    }
}