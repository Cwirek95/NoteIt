using MediatR;
using NoteIt.Application.Responses;

namespace NoteIt.Application.Functions.Contacts.Commands.UpdateContact
{
    public class UpdateContactCommand : IRequest<ICommandResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? EmailAddress { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
