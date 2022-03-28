using MediatR;

namespace NoteIt.Application.Functions.Events.Queries.GetEventsListByStorage
{
    public class GetEventsListByStorageQuery : IRequest<IEnumerable<EventsInListByStorageViewModel>>
    {
        public Guid StorageId { get; set; }
    }
}
