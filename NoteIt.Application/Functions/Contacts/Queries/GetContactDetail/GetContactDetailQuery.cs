using MediatR;
using NoteIt.Application.Caching;

namespace NoteIt.Application.Functions.Contacts.Queries.GetContactDetail
{
    public class GetContactDetailQuery : IRequest<ContactDetailViewModel>, ICacheable
    {
        public int Id { get; set; }


        public string StorageAddress { get; set; }


        public string CacheKey => $"GetContactDetail-{Id}";
    }
}
