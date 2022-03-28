using NoteIt.Domain.Entities;

namespace NoteIt.Application.Contracts.Repositories
{
    public interface IStorageRepository : IAsyncRepository<Storage>
    {
        Task<Storage> GetByNameAsync(string name);
    }
}
