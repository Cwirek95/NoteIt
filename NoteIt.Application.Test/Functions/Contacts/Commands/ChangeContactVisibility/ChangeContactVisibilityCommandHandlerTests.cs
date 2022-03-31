using Xunit;
using System.Threading.Tasks;
using NoteIt.Application.Contracts.Repositories;
using Moq;
using NoteIt.Application.Test.Mocks.Repositories;
using System.Threading;
using FluentAssertions;

namespace NoteIt.Application.Functions.Contacts.Commands.ChangeContactVisibility.Tests
{
    public class ChangeContactVisibilityCommandHandlerTests
    {
        private Mock<IContactRepository> _contactRepositoryMock;

        public ChangeContactVisibilityCommandHandlerTests()
        {
            _contactRepositoryMock = ContactRepositoryMock.GetContactRepository();
        }

        [Fact()]
        public async Task Handle_ForValidCommand_ReturnSuccessResponse()
        {
            // Arrange
            var handler = new ChangeContactVisibilityCommandHandler(_contactRepositoryMock.Object);
            var command = new ChangeContactVisibilityCommand()
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
        public async Task Handle_ChangeVisibility_ReturnContactWithChangedVisibility(int id)
        {
            // Arrange
            var handler = new ChangeContactVisibilityCommandHandler(_contactRepositoryMock.Object);
            var existItem = (await _contactRepositoryMock.Object.GetByIdAsync(id)).IsHidden;
            var command = new ChangeContactVisibilityCommand()
            {
                Id = 1,
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);
            var updatedItem = await _contactRepositoryMock.Object.GetByIdAsync(1);

            // Assert
            updatedItem.IsHidden.Should().NotBe(existItem);
        }
    }
}