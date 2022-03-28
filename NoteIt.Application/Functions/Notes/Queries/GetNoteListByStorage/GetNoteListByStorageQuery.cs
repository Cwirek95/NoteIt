using MediatR;

namespace NoteIt.Application.Functions.Notes.Queries.GetNoteListByStorage
{
    public class GetNoteListByStorageQuery : IRequest<IEnumerable<NoteListByStorageViewModel>>
    {
        public Guid StorageId { get; set; }
    }
}
