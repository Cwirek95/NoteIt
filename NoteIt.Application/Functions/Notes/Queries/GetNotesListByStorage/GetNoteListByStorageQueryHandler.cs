using AutoMapper;
using MediatR;
using NoteIt.Application.Contracts.Repositories;

namespace NoteIt.Application.Functions.Notes.Queries.GetNotesListByStorage
{
    public class GetNoteListByStorageQueryHandler : IRequestHandler<GetNoteListByStorageQuery, IEnumerable<NotesInListByStorageViewModel>>
    {
        private readonly INoteRepository _noteRepository;
        private readonly IMapper _mapper;

        public GetNoteListByStorageQueryHandler(INoteRepository noteRepository, IMapper mapper)
        {
            _noteRepository = noteRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<NotesInListByStorageViewModel>> Handle(GetNoteListByStorageQuery request, CancellationToken cancellationToken)
        {
            var notes = await _noteRepository.GetAllAsyncByStorageId(request.StorageId);

            return _mapper.Map<IEnumerable<NotesInListByStorageViewModel>>(notes);
        }
    }
}
