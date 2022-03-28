using AutoMapper;
using MediatR;
using NoteIt.Application.Contracts.Repositories;
using NoteIt.Application.Responses;
using NoteIt.Domain.Entities;

namespace NoteIt.Application.Functions.Contacts.Commands.CreateContact
{
    public class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, ICommandResponse>
    {
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;

        public CreateContactCommandHandler(IContactRepository contactRepository, IMapper mapper)
        {
            _contactRepository = contactRepository;
            _mapper = mapper;
        }

        public async Task<ICommandResponse> Handle(CreateContactCommand request, CancellationToken cancellationToken)
        {
            var contact = _mapper.Map<Contact>(request);

            contact = await _contactRepository.AddAsync(contact);

            return new CommandResponse(contact.Id);
        }
    }
}
