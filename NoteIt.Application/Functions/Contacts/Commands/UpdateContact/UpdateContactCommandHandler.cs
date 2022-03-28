using MediatR;
using NoteIt.Application.Contracts.Repositories;
using NoteIt.Application.Responses;

namespace NoteIt.Application.Functions.Contacts.Commands.UpdateContact
{
    public class UpdateContactCommandHandler : IRequestHandler<UpdateContactCommand, ICommandResponse>
    {
        private readonly IContactRepository _contactRepository;

        public UpdateContactCommandHandler(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<ICommandResponse> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
        {
            var contact = await _contactRepository.GetByIdAsync(request.Id);

            contact.Name = request.Name;
            contact.EmailAddress = request.EmailAddress;
            contact.PhoneNumber = request.PhoneNumber;

            await _contactRepository.UpdateAsync(contact);

            return new CommandResponse();
        }
    }
}
