using MediatR;
using NoteIt.Application.Contracts.Repositories;
using NoteIt.Application.Responses;

namespace NoteIt.Application.Functions.Contacts.Commands.ChangeContactVisibility
{
    public class ChangeContactVisibilityCommandHandler : IRequestHandler<ChangeContactVisibilityCommand, ICommandResponse>
    {
        private readonly IContactRepository _contactRepository;

        public ChangeContactVisibilityCommandHandler(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<ICommandResponse> Handle(ChangeContactVisibilityCommand request, CancellationToken cancellationToken)
        {
            var contact = await _contactRepository.GetByIdAsync(request.Id);

            contact.IsHidden = !contact.IsHidden;

            await _contactRepository.UpdateAsync(contact);

            return new CommandResponse();
        }
    }
}
