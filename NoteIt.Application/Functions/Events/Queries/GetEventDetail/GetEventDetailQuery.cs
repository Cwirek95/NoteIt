using MediatR;

namespace NoteIt.Application.Functions.Events.Queries.GetEventDetail
{
    public class GetEventDetailQuery : IRequest<EventDetailViewModel>
    {
        public int Id { get; set; }


        public string StorageAddress { get; set; }
    }
}
