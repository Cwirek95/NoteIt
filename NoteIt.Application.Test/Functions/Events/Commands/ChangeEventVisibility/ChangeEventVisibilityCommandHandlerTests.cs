using Xunit;
using System.Threading.Tasks;
using System.Threading;
using FluentAssertions;
using NoteIt.Application.Contracts.Repositories;
using Moq;
using NoteIt.Application.Test.Mocks.Repositories;

namespace NoteIt.Application.Functions.Events.Commands.ChangeEventVisibility.Tests
{
    public class ChangeEventVisibilityCommandHandlerTests
    {
        private Mock<IEventRepository> _eventRepositoryMock;

        public ChangeEventVisibilityCommandHandlerTests()
        {
            _eventRepositoryMock = EventRepositoryMock.GetEventRepository();
        }

        [Fact()]
        public async Task Handle_ForValidCommand_ReturnSuccessResponse()
        {
            // Arrange
            var handler = new ChangeEventVisibilityCommandHandler(_eventRepositoryMock.Object);
            var command = new ChangeEventVisibilityCommand()
            {
                Id = 1
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
        public async Task Handle_ChangeVisibility_ReturnEventWithChangedVisibility(int id)
        {
            // Arrange
            var handler = new ChangeEventVisibilityCommandHandler(_eventRepositoryMock.Object);
            var existItem = (await _eventRepositoryMock.Object.GetByIdAsync(id)).IsHidden;
            var command = new ChangeEventVisibilityCommand()
            {
                Id = 1,
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);
            var updatedItem = await _eventRepositoryMock.Object.GetByIdAsync(1);

            // Assert
            updatedItem.IsHidden.Should().NotBe(existItem);
        }
    }
}