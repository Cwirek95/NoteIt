using Microsoft.EntityFrameworkCore;
using NoteIt.Application.Contracts.Repositories;
using NoteIt.Domain.Entities;

namespace NoteIt.Persistence.EF.Repositories
{
    public class NoteRepository : BaseRepository<Note>, INoteRepository
    {
        public NoteRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IReadOnlyList<Note>> GetAllAsyncByStorageId(Guid id)
        {
            var notes = await _context.Notes.Include(x => x.Storage).Where(x => x.StorageId == id).ToListAsync();

            return notes;
        }
    }
}
