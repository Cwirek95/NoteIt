using MediatR;
using NoteIt.Application.Caching;

namespace NoteIt.Application.Functions.Events.Queries.GetEventDetail
{
    public class GetEventDetailQuery : IRequest<EventDetailViewModel>, ICacheable
    {
        public int Id { get; set; }


        public string StorageAddress { get; set; }


        public string CacheKey => $"GetEventDetail-{Id}";
    }
}
