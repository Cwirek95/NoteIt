using Microsoft.EntityFrameworkCore;
using NoteIt.Application.Contracts.Repositories;
using NoteIt.Domain.Entities;

namespace NoteIt.Persistence.EF.Repositories
{
    public class ContactRepository : BaseRepository<Contact>, IContactRepository
    {
        public ContactRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IReadOnlyList<Contact>> GetAllAsyncByStorageId(Guid id)
        {
            var contacts = await _context.Contacts.Include(x => x.Storage).Where(x => x.StorageId == id).ToListAsync();

            return contacts;
        }
    }
}
