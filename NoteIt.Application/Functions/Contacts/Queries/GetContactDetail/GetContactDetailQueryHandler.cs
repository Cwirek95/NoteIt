using AutoMapper;
using MediatR;
using NoteIt.Application.Contracts.Repositories;

namespace NoteIt.Application.Functions.Contacts.Queries.GetContactDetail
{
    public class GetContactDetailQueryHandler : IRequestHandler<GetContactDetailQuery, ContactDetailViewModel>
    {
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;

        public GetContactDetailQueryHandler(IContactRepository contactRepository, IMapper mapper)
        {
            _contactRepository = contactRepository;
            _mapper = mapper;
        }

        public async Task<ContactDetailViewModel> Handle(GetContactDetailQuery request, CancellationToken cancellationToken)
        {
            var contact = await _contactRepository.GetByIdAsync(request.Id);

            return _mapper.Map<ContactDetailViewModel>(contact);
        }
    }
}
