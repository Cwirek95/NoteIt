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
        private readonly IStorageRepository _storageRepository;
        private readonly IMapper _mapper;

        public CreateEventCommandHandler(IEventRepository eventRepository, IMapper mapper, IStorageRepository storageRepository)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
            _storageRepository = storageRepository;
        }

        public async Task<ICommandResponse> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var evt = _mapper.Map<Event>(request);

            var storage = await _storageRepository.GetByAddressAsync(request.StorageAddress);

            evt.StorageId = storage.Id;

            evt = await _eventRepository.AddAsync(evt);

            return new CommandResponse(evt.Id);
        }
    }
}
