using MediatR;
using NoteIt.Application.Responses;

namespace NoteIt.Application.Functions.Notes.Commands.ChangeNoteVisibility
{
    public class ChangeNoteVisibilityCommand : IRequest<ICommandResponse>
    {
        public int Id { get; set; }
    }
}
