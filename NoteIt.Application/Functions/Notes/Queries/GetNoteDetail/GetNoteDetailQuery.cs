using MediatR;
using NoteIt.Application.Caching;

namespace NoteIt.Application.Functions.Notes.Queries.GetNoteDetail
{
    public class GetNoteDetailQuery : IRequest<NoteDetailViewModel>, ICacheable
    {
        public int Id { get; set; }


        public string StorageAddress { get; set; }


        public string CacheKey => $"GetNoteDetail-{Id}";
    }
}
