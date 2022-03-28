using AutoMapper;
using MediatR;
using NoteIt.Application.Contracts.Repositories;
using NoteIt.Application.Responses;
using NoteIt.Domain.Entities;

namespace NoteIt.Application.Functions.Notes.Commands.CreateNote
{
    public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, ICommandResponse>
    {
        private readonly INoteRepository _noteRepository;
        private readonly IMapper _mapper;

        public CreateNoteCommandHandler(INoteRepository noteRepository, IMapper mapper)
        {
            _noteRepository = noteRepository;
            _mapper = mapper;
        }

        public async Task<ICommandResponse> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
        {
            var note = _mapper.Map<Note>(request);

            note = await _noteRepository.AddAsync(note);

            return new CommandResponse(note.Id);
        }
    }
}
