using MediatR;
using NoteIt.Application.Responses;

namespace NoteIt.Application.Functions.Contacts.Commands.DeleteContact
{
    public class DeleteContactCommand : IRequest<ICommandResponse>
    {
        public int Id { get; set; }


        public string StorageAddress { get; set; }
    }
}
