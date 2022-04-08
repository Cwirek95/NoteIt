using MediatR;
using NoteIt.Application.Responses;

namespace NoteIt.Application.Functions.Events.Commands.UpdateEvent
{
    public class UpdateEventCommand : IRequest<ICommandResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Location { get; set; }
        public string? Description { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public DateTimeOffset? ReminderDate { get; set; }


        public string StorageAddress { get; set; }
    }
}
