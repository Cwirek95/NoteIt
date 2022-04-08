using MediatR;
using NoteIt.Application.Caching;

namespace NoteIt.Application.Functions.Notes.Queries.GetNotesListByStorage
{
    public class GetNotesListByStorageQuery : IRequest<IEnumerable<NotesInListByStorageViewModel>>, ICacheable
    {
        public string StorageAddress { get; set; }


        public string CacheKey => $"GetNotesListByStorage-{StorageAddress}";
    }
}
