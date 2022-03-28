using MediatR;

namespace NoteIt.Application.Functions.Contacts.Queries.GetContactsListByStorage
{
    public class GetContactsListByStorageQuery : IRequest<IEnumerable<ContactsInListByStorageViewModel>>
    {
        public Guid StorageId { get; set; }
    }
}
