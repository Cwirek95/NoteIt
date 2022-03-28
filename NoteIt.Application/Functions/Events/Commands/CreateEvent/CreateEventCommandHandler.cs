using AutoMapper;
using MediatR;
using NoteIt.Application.Contracts.Repositories;
using NoteIt.Application.Responses;
using NoteIt.Domain.Entities;

namespace NoteIt.Application.Functions.Events.Commands.CreateEvent
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, ICommandResponse>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;

        public CreateEventCommandHandler(IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public async Task<ICommandResponse> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var evt = _mapper.Map<Event>(request);
            
            evt = await _eventRepository.AddAsync(evt);

            return new CommandResponse(evt.Id);
        }
    }
}
