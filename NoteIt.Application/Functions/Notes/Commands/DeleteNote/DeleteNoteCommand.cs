using MediatR;
using NoteIt.Application.Responses;

namespace NoteIt.Application.Functions.Notes.Commands.DeleteNote
{
    public class DeleteNoteCommand : IRequest<ICommandResponse>
    {
        public int Id { get; set; }
        public string StorageAddress { get; set; }
    }
}
