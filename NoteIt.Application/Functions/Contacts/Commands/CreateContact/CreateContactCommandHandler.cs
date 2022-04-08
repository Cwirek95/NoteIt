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
        private readonly IStorageRepository _storageRepository;
        private readonly IMapper _mapper;

        public CreateContactCommandHandler(IContactRepository contactRepository, IMapper mapper, IStorageRepository storageRepository)
        {
            _contactRepository = contactRepository;
            _mapper = mapper;
            _storageRepository = storageRepository;
        }

        public async Task<ICommandResponse> Handle(CreateContactCommand request, CancellationToken cancellationToken)
        {
            var contact = _mapper.Map<Contact>(request);

            var storage = await _storageRepository.GetByAddressAsync(request.StorageAddress);

            contact.Id = contact.Id;

            contact = await _contactRepository.AddAsync(contact);

            return new CommandResponse(contact.Id);
        }
    }
}
