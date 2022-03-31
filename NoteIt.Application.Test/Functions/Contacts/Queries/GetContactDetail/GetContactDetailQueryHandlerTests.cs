using Xunit;
using System.Threading.Tasks;
using NoteIt.Application.Contracts.Repositories;
using Moq;
using AutoMapper;
using NoteIt.Application.Mapper;
using NoteIt.Application.Test.Mocks.Repositories;
using System.Threading;
using FluentAssertions;

namespace NoteIt.Application.Functions.Contacts.Queries.GetContactDetail.Tests
{
    public class GetContactDetailQueryHandlerTests
    {
        private Mock<IContactRepository> _contactRepositoryMock;
        private IMapper _mapper;

        public GetContactDetailQueryHandlerTests()
        {
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _contactRepositoryMock = ContactRepositoryMock.GetContactRepository();
            _mapper = configurationProvider.CreateMapper();
        }

        [Fact()]
        public async Task Handle_ForValidQuery_ReturnResultOfTypeContactDetailViewModel()
        {
            // Arrange
            var handler = new GetContactDetailQueryHandler(_contactRepositoryMock.Object, _mapper);
            var command = new GetContactDetailQuery()
            {
                Id = 1,
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Should().BeOfType<ContactDetailViewModel>();
        }
    }
}