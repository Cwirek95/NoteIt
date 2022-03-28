using MediatR;
using NoteIt.Application.Responses;

namespace NoteIt.Application.Functions.Contacts.Commands.CreateContact
{
    public class CreateContactCommand : IRequest<ICommandResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? EmailAddress { get; set; }
        public string? PhoneNumber { get; set; }


        public Guid StorageId { get; set; }
    }
}
