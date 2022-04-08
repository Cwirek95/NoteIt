using AutoMapper;
using MediatR;
using NoteIt.Application.Contracts.Repositories;

namespace NoteIt.Application.Functions.Events.Queries.GetEventsListByStorage
{
    public class GetEventsListByStorageQueryHandler : IRequestHandler<GetEventsListByStorageQuery, IEnumerable<EventsInListByStorageViewModel>>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IStorageRepository _storageRepository;
        private readonly IMapper _mapper;

        public GetEventsListByStorageQueryHandler(IEventRepository eventRepository, IMapper mapper, IStorageRepository storageRepository)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
            _storageRepository = storageRepository;
        }

        public async Task<IEnumerable<EventsInListByStorageViewModel>> Handle(GetEventsListByStorageQuery request, CancellationToken cancellationToken)
        {
            var storage = await _storageRepository.GetByAddressAsync(request.StorageAddress);

            var events = await _eventRepository.GetAllAsyncByStorageId(storage.Id);

            return _mapper.Map<IEnumerable<EventsInListByStorageViewModel>>(events);
        }
    }
}
