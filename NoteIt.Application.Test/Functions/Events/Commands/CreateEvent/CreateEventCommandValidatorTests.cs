using Xunit;
using System;
using FluentAssertions;

namespace NoteIt.Application.Functions.Events.Commands.CreateEvent.Tests
{
    public class CreateEventCommandValidatorTests
    {
        [Fact]
        public void Validate_ForEmptyNameReturnInvalidValidation()
        {
            // Arrange
            var validator = new CreateEventCommandValidator();
            var command = new CreateEventCommand()
            {
                Id = 100,
                Name = "",
                Location = "Location100",
                Description = "Desc100",
                StartDate = DateTimeOffset.Now.AddMinutes(-30),
                EndDate = DateTimeOffset.Now.AddMinutes(10),
                ReminderDate = DateTimeOffset.Now.AddMinutes(-55),
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
            var validator = new CreateEventCommandValidator();
            var command = new CreateEventCommand()
            {
                Id = 100,
                Name = new string('A', 257),
                Location = "Location100",
                Description = "Desc100",
                StartDate = DateTimeOffset.Now.AddMinutes(-30),
                EndDate = DateTimeOffset.Now.AddMinutes(10),
                ReminderDate = DateTimeOffset.Now.AddMinutes(-55),
                StorageId = Guid.NewGuid()
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
            var validator = new CreateEventCommandValidator();
            var command = new CreateEventCommand()
            {
                Id = 100,
                Name = "Name100",
                Location = new string('A', 257),
                Description = "Desc100",
                StartDate = DateTimeOffset.Now.AddMinutes(-30),
                EndDate = DateTimeOffset.Now.AddMinutes(10),
                ReminderDate = DateTimeOffset.Now.AddMinutes(-55),
                StorageId = Guid.NewGuid()
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
            var validator = new CreateEventCommandValidator();
            var command = new CreateEventCommand()
            {
                Id = 100,
                Name = "Name100",
                Location = "Location100",
                Description = new string('A', 4001),
                StartDate = DateTimeOffset.Now.AddMinutes(-30),
                EndDate = DateTimeOffset.Now.AddMinutes(10),
                ReminderDate = DateTimeOffset.Now.AddMinutes(-55),
                StorageId = Guid.NewGuid()
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
            var validator = new CreateEventCommandValidator();
            var command = new CreateEventCommand()
            {
                Id = 100,
                Name = "Name100",
                Location = "Location100",
                Description = "Desc100",
                EndDate = DateTimeOffset.Now.AddMinutes(10),
                ReminderDate = DateTimeOffset.Now.AddMinutes(-55),
                StorageId = Guid.NewGuid()
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
            var validator = new CreateEventCommandValidator();
            var command = new CreateEventCommand()
            {
                Id = 100,
                Name = "Name100",
                Location = "Location100",
                Description = "Desc100",
                StartDate = DateTimeOffset.Now.AddMinutes(-60),
                EndDate = DateTimeOffset.Now.AddMinutes(10),
                ReminderDate = DateTimeOffset.Now.AddMinutes(-55),
                StorageId = Guid.NewGuid()
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
            var validator = new CreateEventCommandValidator();
            var command = new CreateEventCommand()
            {
                Id = 100,
                Name = "Name100",
                Location = "Location100",
                Description = "Desc100",
                StartDate = DateTimeOffset.Now.AddMinutes(10),
                ReminderDate = DateTimeOffset.Now.AddMinutes(-55),
                StorageId = Guid.NewGuid()
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
            var validator = new CreateEventCommandValidator();
            var command = new CreateEventCommand()
            {
                Id = 100,
                Name = "Name100",
                Location = "Location100",
                Description = "Desc100",
                StartDate = DateTimeOffset.Now.AddMinutes(60),
                EndDate = DateTimeOffset.Now.AddMinutes(10),
                ReminderDate = DateTimeOffset.Now.AddMinutes(-55),
                StorageId = Guid.NewGuid()
            };
            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Validate_ForEmptyStorageIdReturnInvalidValidation()
        {
            // Arrange
            var validator = new CreateEventCommandValidator();
            var command = new CreateEventCommand()
            {
                Id = 100,
                Name = "Name100",
                Location = "Location100",
                Description = "Desc100",
                StartDate = DateTimeOffset.Now.AddMinutes(10),
                EndDate = DateTimeOffset.Now.AddMinutes(30),
                ReminderDate = DateTimeOffset.Now.AddMinutes(-55),
                StorageId = Guid.Empty
            };
            // Act
            var response = validator.Validate(command);

            // Assert
            response.IsValid.Should().BeFalse();
        }
    }
}