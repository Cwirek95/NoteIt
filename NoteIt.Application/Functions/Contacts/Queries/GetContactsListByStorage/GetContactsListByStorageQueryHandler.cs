using AutoMapper;
using MediatR;
using NoteIt.Application.Contracts.Repositories;

namespace NoteIt.Application.Functions.Contacts.Queries.GetContactsListByStorage
{
    public class GetContactsListByStorageQueryHandler : IRequestHandler<GetContactsListByStorageQuery, IEnumerable<ContactsInListByStorageViewModel>>
    {
        private readonly IContactRepository _contactRepository;
        private readonly IStorageRepository _storageRepository;
        private readonly IMapper _mapper;

        public GetContactsListByStorageQueryHandler(IContactRepository contactRepository, IMapper mapper, IStorageRepository storageRepository)
        {
            _contactRepository = contactRepository;
            _mapper = mapper;
            _storageRepository = storageRepository;
        }

        public async Task<IEnumerable<ContactsInListByStorageViewModel>> Handle(GetContactsListByStorageQuery request, CancellationToken cancellationToken)
        {
            var storage = await _storageRepository.GetByAddressAsync(request.StorageAddress);

            var contacts = await _contactRepository.GetAllAsyncByStorageId(storage.Id);

            return _mapper.Map<IEnumerable<ContactsInListByStorageViewModel>>(contacts);
        }
    }
}
