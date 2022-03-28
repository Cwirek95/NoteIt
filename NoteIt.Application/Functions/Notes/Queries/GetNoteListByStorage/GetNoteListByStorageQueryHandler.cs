using AutoMapper;
using MediatR;
using NoteIt.Application.Contracts.Repositories;

namespace NoteIt.Application.Functions.Notes.Queries.GetNoteListByStorage
{
    public class GetNoteListByStorageQueryHandler : IRequestHandler<GetNoteListByStorageQuery, IEnumerable<NoteListByStorageViewModel>>
    {
        private readonly INoteRepository _noteRepository;
        private readonly IMapper _mapper;

        public GetNoteListByStorageQueryHandler(INoteRepository noteRepository, IMapper mapper)
        {
            _noteRepository = noteRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<NoteListByStorageViewModel>> Handle(GetNoteListByStorageQuery request, CancellationToken cancellationToken)
        {
            var note = _noteRepository.GetAllAsyncByStorageId(request.StorageId);

            return _mapper.Map<IEnumerable<NoteListByStorageViewModel>>(note);
        }
    }
}
