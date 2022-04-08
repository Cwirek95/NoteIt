using MediatR;
using NoteIt.Application.Caching;

namespace NoteIt.Application.Functions.Contacts.Queries.GetContactsListByStorage
{
    public class GetContactsListByStorageQuery : IRequest<IEnumerable<ContactsInListByStorageViewModel>>, ICacheable
    {
        public string StorageAddress { get; set; }


        public string CacheKey => $"GetContactsListByStorage-{StorageAddress}";
    }
}
