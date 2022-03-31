using Xunit;
using System.Threading.Tasks;
using NoteIt.Application.Contracts.Repositories;
using Moq;
using AutoMapper;
using NoteIt.Application.Mapper;
using NoteIt.Application.Test.Mocks.Repositories;
using System.Threading;
using FluentAssertions;

namespace NoteIt.Application.Functions.Events.Queries.GetEventDetail.Tests
{
    public class GetEventDetailQueryHandlerTests
    {
        private Mock<IEventRepository> _eventRepositoryMock;
        private IMapper _mapper;

        public GetEventDetailQueryHandlerTests()
        {
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _eventRepositoryMock = EventRepositoryMock.GetEventRepository();
            _mapper = configurationProvider.CreateMapper();
        }

        [Fact()]
        public async Task Handle_ForValidQuery_ReturnResultOfTypeEventDetailViewModel()
        {
            // Arrange
            var handler = new GetEventDetailQueryHandler(_eventRepositoryMock.Object, _mapper);
            var command = new GetEventDetailQuery()
            {
                Id = 1,
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Should().BeOfType<EventDetailViewModel>();
        }
    }
}