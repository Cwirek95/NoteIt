using MediatR;
using NoteIt.Application.Responses;

namespace NoteIt.Application.Functions.Notes.Commands.CreateNote
{
    public class CreateNoteCommand : IRequest<ICommandResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Content { get; set; }
        public bool IsImportant { get; set; }


        public string StorageAddress { get; set; }
    }
}
