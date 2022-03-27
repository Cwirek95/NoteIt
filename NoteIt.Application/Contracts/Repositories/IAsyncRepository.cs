namespace NoteIt.Application.Contracts.Repositories
{
    public interface IAsyncRepository<TEntity> where TEntity : class
    {
        Task<IReadOnlyList<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int id);
        Task<TEntity> AddAsync(int id);
        Task UpdateAsync(int id);
        Task DeleteAsync(int id);
    }
}
