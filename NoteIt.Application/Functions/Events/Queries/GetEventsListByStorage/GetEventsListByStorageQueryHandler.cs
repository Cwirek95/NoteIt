using AutoMapper;
using MediatR;
using NoteIt.Application.Contracts.Repositories;

namespace NoteIt.Application.Functions.Events.Queries.GetEventsListByStorage
{
    public class GetEventsListByStorageQueryHandler : IRequestHandler<GetEventsListByStorageQuery, IEnumerable<EventsInListByStorageViewModel>>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;

        public GetEventsListByStorageQueryHandler(IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EventsInListByStorageViewModel>> Handle(GetEventsListByStorageQuery request, CancellationToken cancellationToken)
        {
            var events = await _eventRepository.GetAllAsyncByStorageId(request.StorageId);

            return _mapper.Map<IEnumerable<EventsInListByStorageViewModel>>(events);
        }
    }
}
