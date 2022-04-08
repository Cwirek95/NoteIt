using MediatR;
using NoteIt.Application.Responses;

namespace NoteIt.Application.Functions.Events.Commands.ChangeEventVisibility
{
    public class ChangeEventVisibilityCommand : IRequest<ICommandResponse>
    {
        public int Id { get; set; }


        public string StorageAddress { get; set; }
    }
}
