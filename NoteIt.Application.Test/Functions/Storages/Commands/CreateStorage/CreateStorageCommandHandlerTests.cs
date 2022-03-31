using Xunit;
using System.Threading.Tasks;
using Moq;
using NoteIt.Application.Test.Mocks.Repositories;
using NoteIt.Application.Contracts.Repositories;
using AutoMapper;
using NoteIt.Application.Mapper;
using System.Threading;
using FluentAssertions;
using System;

namespace NoteIt.Application.Functions.Storages.Commands.CreateStorage.Tests
{
    public class CreateStorageCommandHandlerTests
    {
        private Mock<IStorageRepository> _storageRepositoryMock;
        private IMapper _mapper;

        public CreateStorageCommandHandlerTests()
        {
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _storageRepositoryMock = StorageRepositoryMock.GetStorageRepository();
            _mapper = configurationProvider.CreateMapper();
        }

        [Fact()]
        public async Task Handle_ForValidCommand_ReturnSuccessResponse()
        {
            // Arrange
            var handler = new CreateStorageCommandHandler(_storageRepositoryMock.Object, _mapper);
            var command = new CreateStorageCommand()
            {
                Id = Guid.NewGuid(),
                Name = "NewStorage",
                Password = "Pass12345"
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Success.Should().BeTrue();
        }

        [Fact()]
        public async Task Handle_CreateStorage_ReturnNewStorageId()
        {
            // Arrange
            var handler = new CreateStorageCommandHandler(_storageRepositoryMock.Object, _mapper);
            var command = new CreateStorageCommand()
            {
                Id = Guid.NewGuid(),
                Name = "NewStorage",
                Password = "Pass12345"
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Id.Should().Be(command.Id);
        }

        [Fact()]
        public async Task Handle_CreateStorage_ReturnOneMoreStorages()
        {
            // Arrange
            var handler = new CreateStorageCommandHandler(_storageRepositoryMock.Object, _mapper);
            var countBefore = (await _storageRepositoryMock.Object.GetAllAsync()).Count;
            var command = new CreateStorageCommand()
            {
                Id = Guid.NewGuid(),
                Name = "NewStorage",
                Password = "Pass12345"
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);
            var countAfter = (await _storageRepositoryMock.Object.GetAllAsync()).Count;

            // Assert
            countAfter.Should().Be(countBefore + 1);
        }
    }
}