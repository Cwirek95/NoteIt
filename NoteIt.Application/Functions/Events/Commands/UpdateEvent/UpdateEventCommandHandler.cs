using MediatR;
using NoteIt.Application.Contracts.Repositories;
using NoteIt.Application.Responses;

namespace NoteIt.Application.Functions.Events.Commands.UpdateEvent
{
    public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, ICommandResponse>
    {
        private readonly IEventRepository _eventRepository;

        public UpdateEventCommandHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<ICommandResponse> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            var evt = await _eventRepository.GetByIdAsync(request.Id);

            evt.Name = request.Name;
            evt.Location = request.Location;
            evt.Description = request.Description;
            evt.StartDate = request.StartDate;
            evt.EndDate = request.EndDate;
            evt.ReminderDate = request.ReminderDate;

            await _eventRepository.UpdateAsync(evt);

            return new CommandResponse();
        }
    }
}
