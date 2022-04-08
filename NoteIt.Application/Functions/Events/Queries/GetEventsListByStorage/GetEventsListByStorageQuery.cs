using MediatR;
using NoteIt.Application.Caching;

namespace NoteIt.Application.Functions.Events.Queries.GetEventsListByStorage
{
    public class GetEventsListByStorageQuery : IRequest<IEnumerable<EventsInListByStorageViewModel>>, ICacheable
    {
        public string StorageAddress { get; set; }


        public string CacheKey => $"GetEventsListByStorage-{StorageAddress}";
    }
}
