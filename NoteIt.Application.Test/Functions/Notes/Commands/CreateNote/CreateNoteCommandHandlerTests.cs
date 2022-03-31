using Xunit;
using System.Threading.Tasks;
using NoteIt.Application.Contracts.Repositories;
using Moq;
using AutoMapper;
using NoteIt.Application.Mapper;
using NoteIt.Application.Test.Mocks.Repositories;
using System.Threading;
using FluentAssertions;
using System;

namespace NoteIt.Application.Functions.Notes.Commands.CreateNote.Tests
{
    public class CreateNoteCommandHandlerTests
    {
        private Mock<INoteRepository> _noteRepositoryMock;
        private IMapper _mapper;

        public CreateNoteCommandHandlerTests()
        {
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _noteRepositoryMock = NoteRepositoryMock.GetNoteRepository();
            _mapper = configurationProvider.CreateMapper();
        }

        [Fact()]
        public async Task Handle_ForValidCommand_ReturnSuccessResponse()
        {
            // Arrange
            var handler = new CreateNoteCommandHandler(_noteRepositoryMock.Object, _mapper);
            var command = new CreateNoteCommand()
            {
                Id = 100,
                Name = "Note100",
                Content = "Content100",
                IsImportant = false,
                StorageId = Guid.NewGuid()
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Success.Should().BeTrue();
        }

        [Fact()]
        public async Task Handle_CreateNote_ReturnNewNoteId()
        {
            // Arrange
            var handler = new CreateNoteCommandHandler(_noteRepositoryMock.Object, _mapper);
            var command = new CreateNoteCommand()
            {
                Id = 100,
                Name = "Note100",
                Content = "Content100",
                IsImportant = false,
                StorageId = Guid.NewGuid()
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Id.Should().Be(command.Id);
        }

        [Fact()]
        public async Task Handle_CreateNote_ReturnOneMoreNotes()
        {
            // Arrange
            var handler = new CreateNoteCommandHandler(_noteRepositoryMock.Object, _mapper);
            var countBefore = (await _noteRepositoryMock.Object.GetAllAsync()).Count;
            var command = new CreateNoteCommand()
            {
                Id = 100,
                Name = "Note100",
                Content = "Content100",
                IsImportant = false,
                StorageId = Guid.NewGuid()
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);
            var countAfter = (await _noteRepositoryMock.Object.GetAllAsync()).Count;

            // Assert
            countAfter.Should().Be(countBefore + 1);
        }
    }
}