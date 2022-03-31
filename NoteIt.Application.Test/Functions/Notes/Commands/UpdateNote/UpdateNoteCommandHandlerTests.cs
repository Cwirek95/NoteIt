using Xunit;
using System.Threading.Tasks;
using NoteIt.Application.Contracts.Repositories;
using Moq;
using NoteIt.Application.Test.Mocks.Repositories;
using System.Threading;
using FluentAssertions;

namespace NoteIt.Application.Functions.Notes.Commands.UpdateNote.Tests
{
    public class UpdateNoteCommandHandlerTests
    {
        private Mock<INoteRepository> _noteRepositoryMock;

        public UpdateNoteCommandHandlerTests()
        {
            _noteRepositoryMock = NoteRepositoryMock.GetNoteRepository();
        }

        [Fact()]
        public async Task Handle_ForValidCommand_ReturnSuccessResponse()
        {
            // Arrange
            var handler = new UpdateNoteCommandHandler(_noteRepositoryMock.Object);
            var command = new UpdateNoteCommand()
            {
                Id = 1,
                Name = "NewName",
                Content = "Content1",
                IsImportant = false
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
        public async Task Handle_UpdateNote_ReturnNoteWithChangedName(int id)
        {
            // Arrange
            var handler = new UpdateNoteCommandHandler(_noteRepositoryMock.Object);
            var existItem = (await _noteRepositoryMock.Object.GetByIdAsync(id)).Name;
            var command = new UpdateNoteCommand()
            {
                Id = 1,
                Name = "NewName",
                Content = "Content1",
                IsImportant = false
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);
            var updatedItem = await _noteRepositoryMock.Object.GetByIdAsync(1);

            // Assert
            updatedItem.Name.Should().NotBe(existItem);
        }
    }
}