using Xunit;
using System.Threading.Tasks;
using NoteIt.Application.Contracts.Repositories;
using Moq;
using NoteIt.Application.Test.Mocks.Repositories;
using System.Threading;
using FluentAssertions;

namespace NoteIt.Application.Functions.Contacts.Commands.DeleteContact.Tests
{
    public class DeleteContactCommandHandlerTests
    {
        private Mock<IContactRepository> _contactRepositoryMock;

        public DeleteContactCommandHandlerTests()
        {
            _contactRepositoryMock = ContactRepositoryMock.GetContactRepository();
        }

        [Fact()]
        public async Task Handle_ForValidCommand_ReturnSuccessResponse()
        {
            // Arrange
            var handler = new DeleteContactCommandHandler(_contactRepositoryMock.Object);
            var command = new DeleteContactCommand()
            {
                Id = 3,
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Success.Should().BeTrue();
        }

        [Fact()]
        public async Task Handle_DeleteContact_ReturnOneLessContacts()
        {
            // Arrange
            var handler = new DeleteContactCommandHandler(_contactRepositoryMock.Object);
            var countBefore = (await _contactRepositoryMock.Object.GetAllAsync()).Count;
            var command = new DeleteContactCommand()
            {
                Id = 3,
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);
            var countAfter = (await _contactRepositoryMock.Object.GetAllAsync()).Count;

            // Assert
            countAfter.Should().Be(countBefore - 1);
        }
    }
}