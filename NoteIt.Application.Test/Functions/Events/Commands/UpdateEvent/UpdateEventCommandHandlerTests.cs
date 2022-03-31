using Xunit;
using System;
using System.Threading.Tasks;
using NoteIt.Application.Contracts.Repositories;
using Moq;
using NoteIt.Application.Test.Mocks.Repositories;
using System.Threading;
using FluentAssertions;

namespace NoteIt.Application.Functions.Events.Commands.UpdateEvent.Tests
{
    public class UpdateEventCommandHandlerTests
    {
        private Mock<IEventRepository> _eventRepositoryMock;

        public UpdateEventCommandHandlerTests()
        {
            _eventRepositoryMock = EventRepositoryMock.GetEventRepository();
        }

        [Fact()]
        public async Task Handle_ForValidCommand_ReturnSuccessResponse()
        {
            // Arrange
            var handler = new UpdateEventCommandHandler(_eventRepositoryMock.Object);
            var command = new UpdateEventCommand()
            {
                Id = 1,
                Name = "UpdatedEvent",
                Location = "Location100",
                Description = "Desc100",
                StartDate = DateTimeOffset.Now.AddMinutes(-30),
                EndDate = DateTimeOffset.Now.AddMinutes(10),
                ReminderDate = DateTimeOffset.Now.AddMinutes(-55),
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Success.Should().BeTrue();
        }

        [Theory()]
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(5)]
        public async Task Handle_UpdateNote_ReturnEventWithChangedName(int id)
        {
            // Arrange
            var handler = new UpdateEventCommandHandler(_eventRepositoryMock.Object);
            var existItem = (await _eventRepositoryMock.Object.GetByIdAsync(id)).Name;
            var command = new UpdateEventCommand()
            {
                Id = 1,
                Name = "UpdatedEvent",
                Location = "Location100",
                Description = "Desc100",
                StartDate = DateTimeOffset.Now.AddMinutes(-30),
                EndDate = DateTimeOffset.Now.AddMinutes(10),
                ReminderDate = DateTimeOffset.Now.AddMinutes(-55),
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);
            var updatedItem = await _eventRepositoryMock.Object.GetByIdAsync(1);

            // Assert
            updatedItem.Name.Should().NotBe(existItem);
        }
    }
}