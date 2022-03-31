using Xunit;
using System.Threading.Tasks;
using NoteIt.Application.Contracts.Repositories;
using Moq;
using NoteIt.Application.Test.Mocks.Repositories;
using System.Threading;
using FluentAssertions;

namespace NoteIt.Application.Functions.Notes.Commands.ChangeNoteVisibility.Tests
{
    public class ChangeNoteVisibilityCommandHandlerTests
    {
        private Mock<INoteRepository> _noteRepositoryMock;

        public ChangeNoteVisibilityCommandHandlerTests()
        {
            _noteRepositoryMock = NoteRepositoryMock.GetNoteRepository();
        }

        [Fact()]
        public async Task Handle_ForValidCommand_ReturnSuccessResponse()
        {
            // Arrange
            var handler = new ChangeNoteVisibilityCommandHandler(_noteRepositoryMock.Object);
            var command = new ChangeNoteVisibilityCommand()
            {
                Id = 1,
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
        public async Task Handle_ChangeVisibility_ReturnNoteWithChangedVisibility(int id)
        {
            // Arrange
            var handler = new ChangeNoteVisibilityCommandHandler(_noteRepositoryMock.Object);
            var existItem = (await _noteRepositoryMock.Object.GetByIdAsync(id)).IsHidden;
            var command = new ChangeNoteVisibilityCommand()
            {
                Id = 1,
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);
            var updatedItem = await _noteRepositoryMock.Object.GetByIdAsync(1);

            // Assert
            updatedItem.IsHidden.Should().NotBe(existItem);
        }
    }
}