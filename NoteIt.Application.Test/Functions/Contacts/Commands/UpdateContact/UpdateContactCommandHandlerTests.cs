using Xunit;
using System.Threading.Tasks;
using NoteIt.Application.Contracts.Repositories;
using Moq;
using NoteIt.Application.Test.Mocks.Repositories;
using System.Threading;
using FluentAssertions;

namespace NoteIt.Application.Functions.Contacts.Commands.UpdateContact.Tests
{
    public class UpdateContactCommandHandlerTests
    {
        private Mock<IContactRepository> _contactRepositoryMock;

        public UpdateContactCommandHandlerTests()
        {
            _contactRepositoryMock = ContactRepositoryMock.GetContactRepository();
        }

        [Fact()]
        public async Task Handle_ForValidCommand_ReturnSuccessResponse()
        {
            // Arrange
            var handler = new UpdateContactCommandHandler(_contactRepositoryMock.Object);
            var command = new UpdateContactCommand()
            {
                Id = 1,
                Name = "UpdatedContact",
                EmailAddress = "Email100@email.com",
                PhoneNumber = "+48987654321"
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
            var handler = new UpdateContactCommandHandler(_contactRepositoryMock.Object);
            var existItem = (await _contactRepositoryMock.Object.GetByIdAsync(id)).Name;
            var command = new UpdateContactCommand()
            {
                Id = 1,
                Name = "UpdatedContact",
                EmailAddress = "Email100@email.com",
                PhoneNumber = "+48987654321"
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);
            var updatedItem = await _contactRepositoryMock.Object.GetByIdAsync(1);

            // Assert
            updatedItem.Name.Should().NotBe(existItem);
        }
    }
}