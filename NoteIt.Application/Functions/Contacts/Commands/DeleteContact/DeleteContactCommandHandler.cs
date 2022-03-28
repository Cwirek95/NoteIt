using MediatR;
using NoteIt.Application.Contracts.Repositories;
using NoteIt.Application.Responses;

namespace NoteIt.Application.Functions.Contacts.Commands.DeleteContact
{
    public class DeleteContactCommandHandler : IRequestHandler<DeleteContactCommand, ICommandResponse>
    {
        private readonly IContactRepository _contactRepository;

        public DeleteContactCommandHandler(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<ICommandResponse> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
        {
            var contact = await _contactRepository.GetByIdAsync(request.Id);

            await _contactRepository.DeleteAsync(contact);

            return new CommandResponse();
        }
    }
}
