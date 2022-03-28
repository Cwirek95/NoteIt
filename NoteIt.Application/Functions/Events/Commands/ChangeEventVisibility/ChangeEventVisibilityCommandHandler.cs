using MediatR;
using NoteIt.Application.Contracts.Repositories;
using NoteIt.Application.Responses;

namespace NoteIt.Application.Functions.Events.Commands.ChangeEventVisibility
{
    public class ChangeEventVisibilityCommandHandler : IRequestHandler<ChangeEventVisibilityCommand, ICommandResponse>
    {
        private readonly IEventRepository _eventRepository;

        public ChangeEventVisibilityCommandHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<ICommandResponse> Handle(ChangeEventVisibilityCommand request, CancellationToken cancellationToken)
        {
            var evt = await _eventRepository.GetByIdAsync(request.Id);

            evt.IsHidden = !evt.IsHidden;

            await _eventRepository.UpdateAsync(evt);

            return new CommandResponse();
        }
    }
}
