using Xunit;
using System.Threading.Tasks;
using NoteIt.Application.Contracts.Repositories;
using Moq;
using NoteIt.Application.Test.Mocks.Repositories;
using System.Threading;
using FluentAssertions;

namespace NoteIt.Application.Functions.Events.Commands.DeleteEvent.Tests
{
    public class DeleteEventCommandHandlerTests
    {
        private Mock<IEventRepository> _eventRepositoryMock;

        public DeleteEventCommandHandlerTests()
        {
            _eventRepositoryMock = EventRepositoryMock.GetEventRepository();
        }

        [Fact()]
        public async Task Handle_ForValidCommand_ReturnSuccessResponse()
        {
            // Arrange
            var handler = new DeleteEventCommandHandler(_eventRepositoryMock.Object);
            var command = new DeleteEventCommand()
            {
                Id = 3,
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Success.Should().BeTrue();
        }

        [Fact()]
        public async Task Handle_DeleteEvent_ReturnOneLessEvents()
        {
            // Arrange
            var handler = new DeleteEventCommandHandler(_eventRepositoryMock.Object);
            var countBefore = (await _eventRepositoryMock.Object.GetAllAsync()).Count;
            var command = new DeleteEventCommand()
            {
                Id = 3,
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);
            var countAfter = (await _eventRepositoryMock.Object.GetAllAsync()).Count;

            // Assert
            countAfter.Should().Be(countBefore - 1);
        }
    }
}