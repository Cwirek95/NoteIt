using MediatR;
using NoteIt.Application.Contracts.Repositories;
using NoteIt.Application.Responses;

namespace NoteIt.Application.Functions.Notes.Commands.UpdateNote
{
    public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand, ICommandResponse>
    {
        private readonly INoteRepository _noteRepository;

        public UpdateNoteCommandHandler(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public async Task<ICommandResponse> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
        {
            var note = await _noteRepository.GetByIdAsync(request.Id);

            note.Name = request.Name;
            note.Content = request.Content;
            note.IsImportant = request.IsImportant;

            await _noteRepository.UpdateAsync(note);

            return new CommandResponse();
        }
    }
}
