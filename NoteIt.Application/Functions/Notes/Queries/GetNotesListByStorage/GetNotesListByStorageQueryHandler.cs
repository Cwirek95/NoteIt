using AutoMapper;
using MediatR;
using NoteIt.Application.Contracts.Repositories;

namespace NoteIt.Application.Functions.Notes.Queries.GetNotesListByStorage
{
    public class GetNotesListByStorageQueryHandler : IRequestHandler<GetNotesListByStorageQuery, IEnumerable<NotesInListByStorageViewModel>>
    {
        private readonly INoteRepository _noteRepository;
        private readonly IStorageRepository _storageRepository;
        private readonly IMapper _mapper;

        public GetNotesListByStorageQueryHandler(INoteRepository noteRepository, IMapper mapper, IStorageRepository storageRepository)
        {
            _noteRepository = noteRepository;
            _mapper = mapper;
            _storageRepository = storageRepository;
        }

        public async Task<IEnumerable<NotesInListByStorageViewModel>> Handle(GetNotesListByStorageQuery request, CancellationToken cancellationToken)
        {
            var storage = await _storageRepository.GetByAddressAsync(request.StorageAddress);

            var notes = await _noteRepository.GetAllAsyncByStorageId(storage.Id);

            return _mapper.Map<IEnumerable<NotesInListByStorageViewModel>>(notes);
        }
    }
}
