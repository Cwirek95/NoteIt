using NoteIt.Domain.Entities;

namespace NoteIt.Application.Contracts.Repositories
{
    public interface IContactRepository : IAsyncRepository<Contact>
    {
        Task<IReadOnlyList<Contact>> GetAllAsyncByStorageId(Guid id);
    }
}
