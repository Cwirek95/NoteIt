using AutoMapper;
using MediatR;
using NoteIt.Application.Contracts.Repositories;

namespace NoteIt.Application.Functions.Events.Queries.GetEventDetail
{
    public class GetEventDetailQueryHandler : IRequestHandler<GetEventDetailQuery, EventDetailViewModel>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;

        public GetEventDetailQueryHandler(IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public async Task<EventDetailViewModel> Handle(GetEventDetailQuery request, CancellationToken cancellationToken)
        {
            var evt = await _eventRepository.GetByIdAsync(request.Id);

            return _mapper.Map<EventDetailViewModel>(evt);
        }
    }
}
