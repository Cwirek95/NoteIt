using Xunit;
using System;
using FluentAssertions;

namespace NoteIt.Application.Functions.Events.Commands.UpdateEvent.Tests
{
    public class UpdateEventCommandValidatorTests
    {
        [Fact]
        public void Validate_ForEmptyNameReturnInvalidValidation()
        {
            // Arrange
            var validator = new UpdateEventCommandValidator();
            var command = new UpdateEventCommand()
            {
                Id = 1,
                Name = "",
                Location = "Location1",
                Description = "Desc1",
                StartDate = DateTimeOffset.Now.AddMinutes(-30),
                EndDate = DateTimeOffset.Now.AddMinutes(10),
                ReminderDate = DateTimeOffset.Now.AddMinutes(-55),
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
            var validator = new UpdateEventCommandValidator();
            var command = new UpdateEventCommand()
            {
                Id = 1,
                Name = new string('A', 257),
                Location = "Location1",
                Description = "Desc1",
                StartDate = DateTimeOffset.Now.AddMinutes(-30),
                EndDate = DateTimeOffset.Now.AddMinutes(10),
                ReminderDate = DateTimeOffset.Now.AddMinutes(-55),
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }

        [Fact()]
        public void Validate_ForTooLongLocationReturnInvalidValidation()
        {
            // Arrange
            var validator = new UpdateEventCommandValidator();
            var command = new UpdateEventCommand()
            {
                Id = 1,
                Name = "Name1",
                Location = new string('A', 257),
                Description = "Desc1",
                StartDate = DateTimeOffset.Now.AddMinutes(-30),
                EndDate = DateTimeOffset.Now.AddMinutes(10),
                ReminderDate = DateTimeOffset.Now.AddMinutes(-55),
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }

        [Fact()]
        public void Validate_ForTooLongDescriptionReturnInvalidValidation()
        {
            // Arrange
            var validator = new UpdateEventCommandValidator();
            var command = new UpdateEventCommand()
            {
                Id = 1,
                Name = "Name1",
                Location = "Location1",
                Description = new string('A', 4001),
                StartDate = DateTimeOffset.Now.AddMinutes(-30),
                EndDate = DateTimeOffset.Now.AddMinutes(10),
                ReminderDate = DateTimeOffset.Now.AddMinutes(-55),
            };

            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Validate_ForEmptyStartDateReturnInvalidValidation()
        {
            // Arrange
            var validator = new UpdateEventCommandValidator();
            var command = new UpdateEventCommand()
            {
                Id = 1,
                Name = "Name1",
                Location = "Location1",
                Description = "Desc1",
                EndDate = DateTimeOffset.Now.AddMinutes(10),
                ReminderDate = DateTimeOffset.Now.AddMinutes(-55),
            };
            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Validate_ForStartDateBeforeReminderDateReturnInvalidValidation()
        {
            // Arrange
            var validator = new UpdateEventCommandValidator();
            var command = new UpdateEventCommand()
            {
                Id = 1,
                Name = "Name1",
                Location = "Location1",
                Description = "Desc1",
                StartDate = DateTimeOffset.Now.AddMinutes(-60),
                EndDate = DateTimeOffset.Now.AddMinutes(10),
                ReminderDate = DateTimeOffset.Now.AddMinutes(-55),
            };
            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Validate_ForEmptyEndDateReturnInvalidValidation()
        {
            // Arrange
            var validator = new UpdateEventCommandValidator();
            var command = new UpdateEventCommand()
            {
                Id = 1,
                Name = "Name1",
                Location = "Location1",
                Description = "Desc1",
                StartDate = DateTimeOffset.Now.AddMinutes(10),
                ReminderDate = DateTimeOffset.Now.AddMinutes(-55),
            };
            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Validate_ForEndDateBeforeStartDateReturnInvalidValidation()
        {
            // Arrange
            var validator = new UpdateEventCommandValidator();
            var command = new UpdateEventCommand()
            {
                Id = 1,
                Name = "Name1",
                Location = "Location1",
                Description = "Desc1",
                StartDate = DateTimeOffset.Now.AddMinutes(60),
                EndDate = DateTimeOffset.Now.AddMinutes(10),
                ReminderDate = DateTimeOffset.Now.AddMinutes(-55),
            };
            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }
    }
}