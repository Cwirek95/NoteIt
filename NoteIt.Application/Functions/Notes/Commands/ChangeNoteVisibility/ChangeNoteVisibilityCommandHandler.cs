using MediatR;
using NoteIt.Application.Contracts.Repositories;
using NoteIt.Application.Responses;

namespace NoteIt.Application.Functions.Notes.Commands.ChangeNoteVisibility
{
    public class ChangeNoteVisibilityCommandHandler : IRequestHandler<ChangeNoteVisibilityCommand, ICommandResponse>
    {
        private readonly INoteRepository _noteRepository;

        public ChangeNoteVisibilityCommandHandler(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public async Task<ICommandResponse> Handle(ChangeNoteVisibilityCommand request, CancellationToken cancellationToken)
        {
            var note = await _noteRepository.GetByIdAsync(request.Id);

            note.IsHidden = !note.IsHidden;

            await _noteRepository.UpdateAsync(note);

            return new CommandResponse();
        }
    }
}
