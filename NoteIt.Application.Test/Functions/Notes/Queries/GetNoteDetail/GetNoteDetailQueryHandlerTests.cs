using Xunit;
using System.Threading.Tasks;
using NoteIt.Application.Contracts.Repositories;
using Moq;
using AutoMapper;
using NoteIt.Application.Mapper;
using NoteIt.Application.Test.Mocks.Repositories;
using System.Threading;
using FluentAssertions;

namespace NoteIt.Application.Functions.Notes.Queries.GetNoteDetail.Tests
{
    public class GetNoteDetailQueryHandlerTests
    {
        private Mock<INoteRepository> _noteRepositoryMock;
        private IMapper _mapper;

        public GetNoteDetailQueryHandlerTests()
        {
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _noteRepositoryMock = NoteRepositoryMock.GetNoteRepository();
            _mapper = configurationProvider.CreateMapper();
        }

        [Fact()]
        public async Task Handle_ForValidQuery_ReturnResultOfTypeNoteDetailViewModel()
        {
            // Arrange
            var handler = new GetNoteDetailQueryHandler(_noteRepositoryMock.Object, _mapper);
            var command = new GetNoteDetailQuery()
            {
                Id = 1,
            };

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Should().BeOfType<NoteDetailViewModel>();
        }
    }
}