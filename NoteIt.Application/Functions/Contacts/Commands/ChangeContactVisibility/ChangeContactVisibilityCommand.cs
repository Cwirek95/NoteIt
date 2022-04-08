using MediatR;
using NoteIt.Application.Responses;

namespace NoteIt.Application.Functions.Contacts.Commands.ChangeContactVisibility
{
    public class ChangeContactVisibilityCommand : IRequest<ICommandResponse>
    {
        public int Id { get; set; }


        public string StorageAddress { get; set; }
    }
}
