using AutoMapper;
using MediatR;
using NoteIt.Application.Contracts.Repositories;

namespace NoteIt.Application.Functions.Notes.Queries.GetNoteDetail
{
    public class GetNoteDetailQueryHandler : IRequestHandler<GetNoteDetailQuery, NoteDetailViewModel>
    {
        private readonly INoteRepository _noteRepository;
        private readonly IMapper _mapper;

        public GetNoteDetailQueryHandler(INoteRepository noteRepository, IMapper mapper)
        {
            _noteRepository = noteRepository;
            _mapper = mapper;
        }

        public async Task<NoteDetailViewModel> Handle(GetNoteDetailQuery request, CancellationToken cancellationToken)
        {
            var note = await _noteRepository.GetByIdAsync(request.Id);

            return _mapper.Map<NoteDetailViewModel>(note);
        }
    }
}
