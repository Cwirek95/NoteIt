using MediatR;
using NoteIt.Application.Contracts.Repositories;
using NoteIt.Application.Responses;

namespace NoteIt.Application.Functions.Events.Commands.DeleteEvent
{
    public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand, ICommandResponse>
    {
        private readonly IEventRepository _eventRepository;

        public DeleteEventCommandHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<ICommandResponse> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            var evt = await _eventRepository.GetByIdAsync(request.Id);

            await _eventRepository.DeleteAsync(evt);

            return new CommandResponse();
        }
    }
}
