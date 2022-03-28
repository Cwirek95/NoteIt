using NoteIt.Domain.Entities;

namespace NoteIt.Application.Contracts.Repositories
{
    public interface INoteRepository : IAsyncRepository<Note>
    {
        Task<IReadOnlyList<Note>> GetAllAsyncByStorageId(Guid id);
    }
}
