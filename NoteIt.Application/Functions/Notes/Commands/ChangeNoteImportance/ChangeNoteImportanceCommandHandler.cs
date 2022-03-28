using MediatR;
using NoteIt.Application.Contracts.Repositories;
using NoteIt.Application.Responses;

namespace NoteIt.Application.Functions.Notes.Commands.ChangeNoteImportance
{
    public class ChangeNoteImportanceCommandHandler : IRequestHandler<ChangeNoteImportanceCommand, ICommandResponse>
    {
        private readonly INoteRepository _noteRepository;

        public ChangeNoteImportanceCommandHandler(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public async Task<ICommandResponse> Handle(ChangeNoteImportanceCommand request, CancellationToken cancellationToken)
        {
            var note = await _noteRepository.GetByIdAsync(request.Id);

            note.IsImportant = !note.IsImportant;

            await _noteRepository.UpdateAsync(note);

            return new CommandResponse();
        }
    }
}
