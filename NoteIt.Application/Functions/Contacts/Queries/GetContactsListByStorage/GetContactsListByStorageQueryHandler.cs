using AutoMapper;
using MediatR;
using NoteIt.Application.Contracts.Repositories;

namespace NoteIt.Application.Functions.Contacts.Queries.GetContactsListByStorage
{
    public class GetContactsListByStorageQueryHandler : IRequestHandler<GetContactsListByStorageQuery, IEnumerable<ContactsInListByStorageViewModel>>
    {
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;

        public GetContactsListByStorageQueryHandler(IContactRepository contactRepository, IMapper mapper)
        {
            _contactRepository = contactRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ContactsInListByStorageViewModel>> Handle(GetContactsListByStorageQuery request, CancellationToken cancellationToken)
        {
            var contacts = await _contactRepository.GetAllAsyncByStorageId(request.StorageId);

            return _mapper.Map<IEnumerable<ContactsInListByStorageViewModel>>(contacts);
        }
    }
}
