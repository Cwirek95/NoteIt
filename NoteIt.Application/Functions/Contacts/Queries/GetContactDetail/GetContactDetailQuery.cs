using MediatR;

namespace NoteIt.Application.Functions.Contacts.Queries.GetContactDetail
{
    public class GetContactDetailQuery : IRequest<ContactDetailViewModel>
    {
        public int Id { get; set; }
    }
}
