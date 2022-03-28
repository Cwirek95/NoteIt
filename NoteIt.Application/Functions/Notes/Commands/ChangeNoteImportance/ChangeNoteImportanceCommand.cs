using MediatR;
using NoteIt.Application.Responses;

namespace NoteIt.Application.Functions.Notes.Commands.ChangeNoteImportance
{
    public class ChangeNoteImportanceCommand : IRequest<ICommandResponse>
    {
        public int Id { get; set; }
    }
}
