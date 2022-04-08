using Xunit;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using NoteIt.Application.Contracts.Repositories;
using NoteIt.Application.Mapper;
using NoteIt.Application.Test.Mocks.Repositories;
using System.Threading;
using FluentAssertions;

namespace NoteIt.Application.Functions.Contacts.Commands.CreateContact.Tests
{
    public class CreateContactCommandHandlerTests
    {
        private Mock<IContactRepository> _contactRepositoryMock;
        private Mock<IStorageRepository> _storageRepositoryMock;
        private IMapper _mapper;

        public CreateContactCommandHandlerTests()
        {
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _contactRepositoryMock = ContactRepositoryMock.GetContactRepository();
            _storageRepositoryMock = StorageRepositoryMock.GetStorageRepository();
            _mapper = configurationProvider.CreateMapper();
        }

        [Fact()]
        public async Task Handle_ForValidCommand_ReturnSuccessResponse()
        {
            // Arrange
            var handler = new CreateContactCommandHandler(_contactRepositoryMock.Object, _mapper, _storageRepositoryMock.Object);
            var command = new CreateContactCommand()
            {
                Id = 100,
                Name = "Contact100",
                EmailAddress = "Email100@email.com",
                PhoneNumber = "+48987654321",
                StorageAddress = "storage1"
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Success.Should().BeTrue();
        }

        [Fact()]
        public async Task Handle_CreateContact_ReturnNewContactId()
        {
            // Arrange
            var handler = new CreateContactCommandHandler(_contactRepositoryMock.Object, _mapper, _storageRepositoryMock.Object);
            var command = new CreateContactCommand()
            {
                Id = 100,
                Name = "Contact100",
                EmailAddress = "Email100@email.com",
                PhoneNumber = "+48987654321",
                StorageAddress = "storage1"
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Id.Should().Be(command.Id);
        }

        [Fact()]
        public async Task Handle_CreateContact_ReturnOneMoreContacts()
        {
            // Arrange
            var handler = new CreateContactCommandHandler(_contactRepositoryMock.Object, _mapper, _storageRepositoryMock.Object);
            var countBefore = (await _contactRepositoryMock.Object.GetAllAsync()).Count;
            var command = new CreateContactCommand()
            {
                Id = 100,
                Name = "Contact100",
                EmailAddress = "Email100@email.com",
                PhoneNumber = "+48987654321",
                StorageAddress = "storage1"
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);
            var countAfter = (await _contactRepositoryMock.Object.GetAllAsync()).Count;

            // Assert
            countAfter.Should().Be(countBefore + 1);
        }
    }
}