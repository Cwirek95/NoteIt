using MediatR;
using NoteIt.Application.Responses;

namespace NoteIt.Application.Functions.Events.Commands.DeleteEvent
{
    public class DeleteEventCommand : IRequest<ICommandResponse>
    {
        public int Id { get; set; }
    }
}
