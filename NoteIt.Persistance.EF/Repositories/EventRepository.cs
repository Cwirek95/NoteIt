using Microsoft.EntityFrameworkCore;
using NoteIt.Application.Contracts.Repositories;
using NoteIt.Domain.Entities;

namespace NoteIt.Persistence.EF.Repositories
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        public EventRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IReadOnlyList<Event>> GetAllAsyncByStorageId(Guid id)
        {
            var events = await _context.Events.Include(x => x.Storage).Where(x => x.StorageId == id).ToListAsync();

            return events;
        }
    }
}
