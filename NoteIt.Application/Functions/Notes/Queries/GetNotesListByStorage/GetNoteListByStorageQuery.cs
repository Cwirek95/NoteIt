using MediatR;

namespace NoteIt.Application.Functions.Notes.Queries.GetNotesListByStorage
{
    public class GetNoteListByStorageQuery : IRequest<IEnumerable<NotesInListByStorageViewModel>>
    {
        public Guid StorageId { get; set; }
    }
}
