using Xunit;
using System.Threading.Tasks;
using NoteIt.Application.Contracts.Repositories;
using Moq;
using NoteIt.Application.Test.Mocks.Repositories;
using System.Threading;
using FluentAssertions;

namespace NoteIt.Application.Functions.Notes.Commands.DeleteNote.Tests
{
    public class DeleteNoteCommandHandlerTests
    {
        private Mock<INoteRepository> _noteRepositoryMock;

        public DeleteNoteCommandHandlerTests()
        {
            _noteRepositoryMock = NoteRepositoryMock.GetNoteRepository();
        }

        [Fact()]
        public async Task Handle_ForValidCommand_ReturnSuccessResponse()
        {
            // Arrange
            var handler = new DeleteNoteCommandHandler(_noteRepositoryMock.Object);
            var command = new DeleteNoteCommand()
            {
                Id = 3,
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Success.Should().BeTrue();
        }

        [Fact()]
        public async Task Handle_DeleteNote_ReturnOneLessNotes()
        {
            // Arrange
            var handler = new DeleteNoteCommandHandler(_noteRepositoryMock.Object);
            var countBefore = (await _noteRepositoryMock.Object.GetAllAsync()).Count;
            var command = new DeleteNoteCommand()
            {
                Id = 3,
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);
            var countAfter = (await _noteRepositoryMock.Object.GetAllAsync()).Count;

            // Assert
            countAfter.Should().Be(countBefore - 1);
        }
    }
}