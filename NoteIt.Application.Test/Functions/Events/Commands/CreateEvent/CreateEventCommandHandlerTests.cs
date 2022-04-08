using Xunit;
using System;
using System.Threading.Tasks;
using Moq;
using NoteIt.Application.Contracts.Repositories;
using AutoMapper;
using NoteIt.Application.Mapper;
using NoteIt.Application.Test.Mocks.Repositories;
using System.Threading;
using FluentAssertions;

namespace NoteIt.Application.Functions.Events.Commands.CreateEvent.Tests
{
    public class CreateEventCommandHandlerTests
    {
        private Mock<IEventRepository> _eventRepositoryMock;
        private Mock<IStorageRepository> _storageRepositoryMock;
        private IMapper _mapper;

        public CreateEventCommandHandlerTests()
        {
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _eventRepositoryMock = EventRepositoryMock.GetEventRepository();
            _storageRepositoryMock = StorageRepositoryMock.GetStorageRepository();
            _mapper = configurationProvider.CreateMapper();
        }

        [Fact()]
        public async Task Handle_ForValidCommand_ReturnSuccessResponse()
        {
            // Arrange
            var handler = new CreateEventCommandHandler(_eventRepositoryMock.Object, _mapper, _storageRepositoryMock.Object);
            var command = new CreateEventCommand()
            {
                Id = 100,
                Name = "Event100",
                Location = "Location100",
                Description = "Desc100",
                StartDate = DateTimeOffset.Now.AddMinutes(-30),
                EndDate = DateTimeOffset.Now.AddMinutes(10),
                ReminderDate = DateTimeOffset.Now.AddMinutes(-55),
                StorageAddress = "storage1"
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Success.Should().BeTrue();
        }

        [Fact()]
        public async Task Handle_CreateEvent_ReturnNewEventId()
        {
            // Arrange
            var handler = new CreateEventCommandHandler(_eventRepositoryMock.Object, _mapper, _storageRepositoryMock.Object);
            var command = new CreateEventCommand()
            {
                Id = 100,
                Name = "Event100",
                Location = "Location100",
                Description = "Desc100",
                StartDate = DateTimeOffset.Now.AddMinutes(-30),
                EndDate = DateTimeOffset.Now.AddMinutes(10),
                ReminderDate = DateTimeOffset.Now.AddMinutes(-55),
                StorageAddress = "storage1"
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Id.Should().Be(command.Id);
        }

        [Fact()]
        public async Task Handle_CreateEvent_ReturnOneMoreEvents()
        {
            // Arrange
            var handler = new CreateEventCommandHandler(_eventRepositoryMock.Object, _mapper, _storageRepositoryMock.Object);
            var countBefore = (await _eventRepositoryMock.Object.GetAllAsync()).Count;
            var command = new CreateEventCommand()
            {
                Id = 100,
                Name = "Event100",
                Location = "Location100",
                Description = "Desc100",
                StartDate = DateTimeOffset.Now.AddMinutes(-30),
                EndDate = DateTimeOffset.Now.AddMinutes(10),
                ReminderDate = DateTimeOffset.Now.AddMinutes(-55),
                StorageAddress = "storage1"
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);
            var countAfter = (await _eventRepositoryMock.Object.GetAllAsync()).Count;

            // Assert
            countAfter.Should().Be(countBefore + 1);
        }
    }
}